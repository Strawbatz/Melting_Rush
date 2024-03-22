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
    [SerializeField] float movementSpeed = 1f;

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
            anchor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //TODO fix this with raycasting
            Debug.Log(anchor);
        }

        if(anchor != Vector2.zero)
        {
            rigidbody.AddForce((anchor-(Vector2)transform.position).normalized * slingAcceleration);
        }

        //rigidbody.velocity = movementVector.normalized*movementSpeed*Time.fixedDeltaTime;
    }
}
