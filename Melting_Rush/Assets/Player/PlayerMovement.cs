using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using AdvancedEditorTools.Attributes;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(Rigidbody2D))]
/// <summary>
/// Controls the players movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float slingAcceleration= 1f;

    [SerializeField] InputActionReference mouseButton;
    [SerializeField] Transform playerGraphics;
    [Header("Anchor Line")]
    [SerializeField] float maxAnchorDistance = 100f; 
    [SerializeField] LayerMask anchorLayers;
    [SerializeField] LineRenderer anchorLineRenderer;

    [Header("Eyes")]
    [SerializeField] Transform eyeTrans;
    [SerializeField] float stareIntensity;
    [SerializeField] SpriteRenderer eyebrowRenderer;
    [BeginFoldout("Eyebrows")]
    [SerializeField] Sprite normal;
    [SerializeField] Sprite angry;
    [SerializeField] Sprite scared;
    [EndFoldout]
    private Rigidbody2D rigidbody;

    private Vector2 anchor = Vector2.zero;

    void Start()
    {
        mouseButton.action.performed += MousePressed;
        mouseButton.action.canceled += MouseReleased;
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void OnDisable()
    {
        mouseButton.action.performed -= MousePressed;
        mouseButton.action.canceled -= MouseReleased;
    }

    void MousePressed(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (mousePos-(Vector2)transform.position).normalized;
        RaycastHit2D hit; 
        hit = Physics2D.Raycast(transform.position, aimDirection, maxAnchorDistance, anchorLayers);
        anchor = hit.point;
    }

    void MouseReleased(InputAction.CallbackContext ctx)
    {
        anchor = Vector2.zero;
    }

    private float effectCoolDown = 0f;
    bool colliding;
    Vector2 lastCollisionDir = Vector2.zero;
    private void OnCollisionEnter2D(Collision2D other) {
        colliding = true;
        Vector2 contactDir = Vector2.zero;
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector2 tmp = (other.GetContact(i).point-(Vector2)transform.position);
            contactDir += new Vector2((tmp.x > 0)?1f:-1f, (tmp.y > 0)?1f:-1f);
        }
        contactDir = new Vector2((contactDir.x == 0)? 0:(contactDir.x > 0)?1f:-1f, (contactDir.y == 0)?0:(contactDir.y > 0)?1f:-1f);
        //Debug.Log("contactDir " + contactDir);


        if(contactDir.Equals(lastCollisionDir))
        {
            //Debug.Log(effectCoolDown);
            if(effectCoolDown > 0)
            {
                effectCoolDown = .5f;
                return;
            }
        }
        lastCollisionDir = contactDir;
        effectCoolDown = .5f;
  
        if(other.gameObject.CompareTag("Bouncy")) {
            SoundManager.instance.PlaySound(SoundManager.Sound.Bounce);
        } else {
            SoundManager.instance.PlaySound(SoundManager.Sound.WallHit);
        }
        StartCoroutine(Angry());
    }

    void OnCollisionExit2D(Collision2D other)
    {
        colliding = false;
    }

    void Update()
    {
        if(!colliding && effectCoolDown > 0f) 
        {
            effectCoolDown -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if(anchor != Vector2.zero)
        {
            rigidbody.AddForce((anchor-(Vector2)transform.position).normalized * slingAcceleration);
        }
    }

    void LateUpdate()
    {
        if(anchor != Vector2.zero)
        {
            anchorLineRenderer.SetPositions(new Vector3[]{new Vector3(transform.position.x, transform.position.y), anchor});
            anchorLineRenderer.enabled = true;

            eyeTrans.localPosition = (anchor - (Vector2)eyeTrans.position).normalized * stareIntensity;
        } else
        {
            anchorLineRenderer.enabled = false;
            eyeTrans.localPosition = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - eyeTrans.position).normalized * stareIntensity;
        }
    }
    IEnumerator Angry ()
    {
        eyebrowRenderer.sprite = angry;
        yield return new WaitForSeconds(1f);
        eyebrowRenderer.sprite = normal;
    }

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.forward, maxAnchorDistance);
    }
    #endif
}
