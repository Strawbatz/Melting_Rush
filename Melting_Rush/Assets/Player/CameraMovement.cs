using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdvancedEditorTools;
using AdvancedEditorTools.Attributes;

/// <summary>
/// Controls the movement of the camera
/// </summary>
public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float cameraSpeed;



    void Update()
    {
        Vector3 targetPos = new Vector3(targetTransform.position.x, targetTransform.position.y, transform.position.z);
        Vector3 moveVector = Vector3.Slerp(transform.position, targetPos,cameraSpeed*Time.deltaTime);
        transform.position = moveVector;
    }
}
