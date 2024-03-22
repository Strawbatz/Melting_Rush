using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
/// <summary>
/// Controls the players movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    [SerializeField] private Vector2 movementVector = Vector2.zero;
    private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        rigidbody.velocity = movementVector.normalized*movementSpeed*Time.fixedDeltaTime;
    }
}
