using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float time;

    [Range(-1, 1)]
    [SerializeField] int rotation;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateAround(gameObject, new Vector3(0,0,rotation), -360, time).setLoopClamp();
    }
}
