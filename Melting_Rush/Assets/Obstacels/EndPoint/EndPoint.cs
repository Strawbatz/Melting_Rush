using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndPoint : MonoBehaviour
{
    public static EndPoint instance;
    public UnityAction onEndLevel;
    public GameObject openFridge;
    public GameObject closedFridge;

    void Awake()
    {
        if(!instance)
        {
            instance = this;
        } else
        {
            Debug.LogError("More than one endpoint in scene");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerPhysics"))
        {
            openFridge.SetActive(false);
            closedFridge.SetActive(true);
            SoundManager.instance.PlaySound(SoundManager.Sound.WallHit);
            Time.timeScale = 0f;
            onEndLevel?.Invoke();
            FindObjectOfType<Melting>()?.DisablePlayer();
        }
    }
}
