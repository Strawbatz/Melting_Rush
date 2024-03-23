using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    List<Vector3> nodes = new List<Vector3>();
    Vector3 target;
    int current;

    void Start()
    {
        current = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            nodes.Add(transform.GetChild(i).position);
        }
        target = nodes[0];
    }
    
    void Update()
    {
        transform.Translate((target-transform.position).normalized*movementSpeed*Time.deltaTime);

        if(Vector2.Distance(transform.position, target) < 0.1f)
        {
            current++;
            current %= nodes.Count;
            target = nodes[current];
        }
    }

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        if(!Application.isPlaying)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Handles.DrawLine(transform.GetChild(i).position, transform.GetChild((i+1)%transform.childCount).position);
            }
        } else
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                Handles.DrawLine(nodes[i], nodes[(i+1)%transform.childCount]);
            }
        }
    }
    #endif
}
