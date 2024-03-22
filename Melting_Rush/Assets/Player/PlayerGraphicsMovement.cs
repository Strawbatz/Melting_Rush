using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the movement of the graphical part of the player
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerGraphicsMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerMovement;
    [SerializeField] float speedScale;
    void LateUpdate()
    {

        Vector3 targetPos = new Vector3(playerMovement.transform.position.x, playerMovement.transform.position.y, transform.position.z);
        Vector3 moveVector = Vector3.Slerp(transform.position, targetPos,playerMovement.velocity.magnitude*speedScale*Time.deltaTime);
        transform.position = moveVector;
    }
}
