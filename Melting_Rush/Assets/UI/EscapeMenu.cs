using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] public GameObject container;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject pauseText;
    [SerializeField] InputActionReference escape;
    [SerializeField] InputActionReference restart;

    private bool animationIsRunning;


    private void Start() {
        escape.action.performed += ToggleMenu;
        restart.action.performed += Restart;
        container.SetActive(false);
        LeanTween.scale(pauseText, Vector3.zero, 0f);
        LeanTween.scale(buttons, -Vector3.zero, 0f);
        animationIsRunning = false;
        
    }

    private void OnDisable() {
        escape.action.performed -= ToggleMenu;
        restart.action.performed -= Restart;
    }

    public void ToggleMenu(InputAction.CallbackContext ctx) {
        ToggleMenu();
    }
    public void ToggleMenu() {
        if(!container.activeSelf) {
            Pause();
        } else {
            Resume();
        }
    }
    
    private void Pause() {
        if(animationIsRunning) return;
        StartCoroutine(AnimationRunning(2f));
        container.SetActive(true);
        Time.timeScale = 0;
        SoundManager.instance.ToggleMusic(true);
        LeanTween.scale(pauseText, Vector3.one, 1.5f).setEase(LeanTweenType.easeOutExpo).setIgnoreTimeScale(true);
        LeanTween.scale(buttons, Vector3.one, 1.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutExpo).setIgnoreTimeScale(true);
    }

    private void Resume() {
        if(animationIsRunning) return;
        animationIsRunning = true;
        LeanTween.scale(pauseText, Vector3.zero, 1f).setEase(LeanTweenType.easeOutExpo).setIgnoreTimeScale(true);
        LeanTween.scale(buttons, Vector3.zero, 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutExpo).setIgnoreTimeScale(true);
        StartCoroutine(WaitThenClose(1.5f));
    }

    public void Restart(InputAction.CallbackContext ctx) {
        Restart();
    }
    public void Restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit() {
        Time.timeScale = 1;
        Application.Quit();
    }

    IEnumerator AnimationRunning(float t) {
        animationIsRunning = true;
        yield return new WaitForSecondsRealtime(t);
        animationIsRunning = false;
    }

    IEnumerator WaitThenClose(float t) {
        yield return new WaitForSecondsRealtime(t);
        container.SetActive(false);
        Time.timeScale = 1;
        SoundManager.instance.ToggleMusic(false);
        animationIsRunning = false;
    }
}
