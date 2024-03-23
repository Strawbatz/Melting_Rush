using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class MeltingZone : MonoBehaviour
{
    [SerializeField] float meltingMod;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerPhysics"))
        {
            FindObjectOfType<Melting>().meltingMod *= meltingMod;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("PlayerPhysics"))
        {
            FindObjectOfType<Melting>().meltingMod/=meltingMod;
        }
    }
}
