using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
/// <summary>
/// Controls the players movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float slingAcceleration= 1f;
    [SerializeField] float maxAnchorDistance = 100f; 
    [SerializeField] LayerMask anchorLayers;

    [SerializeField] private Vector2 movementVector = Vector2.zero;
    private Rigidbody2D rigidbody;

    private Vector2 anchor = Vector2.zero;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimDirection = (mousePos-(Vector2)transform.position).normalized;
            RaycastHit2D hit; 
            hit = Physics2D.Raycast(transform.position, aimDirection, maxAnchorDistance, anchorLayers);
            anchor = hit.point;
            Debug.Log(anchor);
        }

        if(anchor != Vector2.zero)
        {
            rigidbody.AddForce((anchor-(Vector2)transform.position).normalized * slingAcceleration);
        }

        //rigidbody.velocity = movementVector.normalized*movementSpeed*Time.fixedDeltaTime;
    }
}
