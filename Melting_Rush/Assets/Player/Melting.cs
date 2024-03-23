using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Melting : MonoBehaviour
{
    [SerializeField] Transform playerGraphics;
    [SerializeField] GameObject playerPhysics;
    private Transform pPhysicsTransform;
    private Rigidbody2D pRigidBody;

    private float startMass;

    private float time = 0;
    [SerializeField] private float minScale;
    [SerializeField] private float startScale;
    [SerializeField] private float massChange;
    [SerializeField] private float maxTime;
    public float meltingMod = 1f;
    private void Start() {
        pPhysicsTransform = playerPhysics.GetComponentInChildren<Transform>();
        pRigidBody = playerPhysics.GetComponentInChildren<Rigidbody2D>();
        startMass = pRigidBody.mass;

        Vector3 startVector = new Vector3(startScale, startScale, startScale);
        playerGraphics.localScale = startVector;
        pPhysicsTransform.localScale = startVector;

        time = 0;
        
    }
    private void Update() {
        time += Time.deltaTime*meltingMod;
        if( time > maxTime) {
            time = maxTime;
        }
        float timeLeft = maxTime - time;
        float procentOfMax = timeLeft/maxTime;
        float currentScale = (startScale-minScale)*procentOfMax + minScale;
        float massDifference = startMass-massChange;

        if(currentScale > minScale) {
            Vector3 scaleVector = new Vector3(1f, currentScale, currentScale);
            playerGraphics.localScale = scaleVector;
            pPhysicsTransform.localScale = scaleVector;
            pRigidBody.mass = startMass*(massDifference*procentOfMax + massChange);
        }
    }
}
