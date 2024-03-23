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
        Debug.Log(anchor);
    }

    void MouseReleased(InputAction.CallbackContext ctx)
    {
        anchor = Vector2.zero;
        Debug.Log("releaseMouse");
    }

    private float effectCoolDown = 0f;
    private void OnCollisionEnter2D(Collision2D other) {

        if(effectCoolDown > 0)
        {
            effectCoolDown = .5f;
            return;
        }
        effectCoolDown = .5f;
  
        SoundManager.instance.PlaySound(SoundManager.Sound.WallHit);
        StartCoroutine(Angry());
    }
    void Update()
    {
        if(effectCoolDown > 0f) effectCoolDown -= Time.deltaTime;
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
