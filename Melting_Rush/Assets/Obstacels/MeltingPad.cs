using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class MeltingPad : MonoBehaviour
{
    [SerializeField] float meltingMod;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("PlayerPhysics"))
        {
            FindObjectOfType<Melting>().meltingMod *= meltingMod;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.collider.CompareTag("PlayerPhysics"))
        {
            FindObjectOfType<Melting>().meltingMod/=meltingMod;
        }
    }
}
