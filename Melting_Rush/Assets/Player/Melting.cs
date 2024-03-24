using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Melting : MonoBehaviour
{
    [SerializeField] Transform playerGraphics;
    [SerializeField] GameObject playerPhysics;
    [SerializeField] SpriteRenderer playerRenderer;
    [SerializeField] GameObject eyes;
    [SerializeField] GameObject anchorLine;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] GameObject[] deathEyes;
    private Transform pPhysicsTransform;
    private Rigidbody2D pRigidBody;

    private float startMass;

    private float time = 0;
    [SerializeField] private float minScale;
    [SerializeField] private float startScale;
    [SerializeField] private float massChange;
    [SerializeField] private float maxTime;

    public bool dead {get; private set;} = false;
    public float meltingMod = 1f;
    private void Start() {
        Time.timeScale = 1;
        pPhysicsTransform = playerPhysics.GetComponentInChildren<Transform>();
        pRigidBody = playerPhysics.GetComponentInChildren<Rigidbody2D>();
        startMass = pRigidBody.mass;

        Vector3 startVector = new Vector3(startScale, startScale, startScale);
        playerGraphics.localScale = startVector;
        pPhysicsTransform.localScale = startVector;

        time = 0;
        SoundManager.instance.PlayMusic(SoundManager.Sound.Chiptune);
        
    }
    private void Update() {
        time += Time.deltaTime*meltingMod;
        SoundManager.instance.SetMusicSpeed(meltingMod);
        if( time > maxTime) {
            time = maxTime;
            if(!dead)
                StartCoroutine(PlayerDeath());
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

    public float GetTime() {
        return time;
    }

    public float GetIceLeft() {
        float timeLeft = maxTime - time;
        return timeLeft/maxTime;
    }

    IEnumerator PlayerDeath()
    {
        if(dead) yield return null;
        dead = true;
        eyes.SetActive(false);
        deathParticles.gameObject.transform.position = playerPhysics.transform.position;
        deathParticles.Play();
        anchorLine.SetActive(false);
        foreach (GameObject eyePrefab in deathEyes)
        {
            GameObject eye = Instantiate(eyePrefab) as GameObject;
            eye.transform.position = playerPhysics.transform.position;
            eye.GetComponent<Rigidbody2D>().velocity = pRigidBody.velocity + new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }
        playerPhysics.SetActive(false);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return null;
    }

    public void DisablePlayer()
    {
        playerPhysics.SetActive(false);
        playerGraphics.gameObject.SetActive(false);
        anchorLine.SetActive(false);
        eyes.SetActive(false);
    }
}
