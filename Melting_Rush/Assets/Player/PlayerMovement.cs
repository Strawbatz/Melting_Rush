using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
/// <summary>
/// Controls the players movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float slingAcceleration= 1f;
    [SerializeField] float maxAnchorDistance = 100f; 
    [SerializeField] LayerMask anchorLayers;
    [SerializeField] InputActionReference mouseButton;
    [SerializeField] LineRenderer anchorLineRenderer;
    [SerializeField] Transform playerGraphics;

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

    void FixedUpdate()
    {
        if(anchor != Vector2.zero)
        {
            rigidbody.AddForce((anchor-(Vector2)transform.position).normalized * slingAcceleration);
        }

        //rigidbody.velocity = movementVector.normalized*movementSpeed*Time.fixedDeltaTime;
    }

    void LateUpdate()
    {
        if(anchor != Vector2.zero)
        {
            anchorLineRenderer.SetPositions(new Vector3[]{new Vector3(playerGraphics.position.x, playerGraphics.position.y), anchor});
            anchorLineRenderer.enabled = true;
        } else
        {
            anchorLineRenderer.enabled = false;
        }
    }
}
