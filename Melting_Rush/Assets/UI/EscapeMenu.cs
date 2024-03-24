using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] public GameObject container;
    [SerializeField] GameObject Buttons;
    [SerializeField] GameObject pauseText;
    [SerializeField] InputActionReference escape;
    [SerializeField] InputActionReference restart;


    private void Start() {
        escape.action.performed += ToggleMenu;
        restart.action.performed += Restart;
        container.SetActive(false);
        //LeanTween.moveLocalY(pauseText, 200, 0);
        //LeanTween.moveLocalY(container, 650, 0);
    }

    private void OnDisable() {
        escape.action.performed -= ToggleMenu;
        restart.action.performed -= Restart;
    }

    public void ToggleMenu(InputAction.CallbackContext ctx) {
        ToggleMenu();
    }
    public void ToggleMenu() {
        container.SetActive(!container.activeSelf);
        if(container.activeSelf) {
            Pause();
        } else {
            Resume();
        }
    }
    
    private void Pause() {
        Time.timeScale = 0;
        SoundManager.instance.ToggleMusic(true);
        //LeanTween.moveLocalY(pauseText, -140, 1.5f).setEase(LeanTweenType.easeInOutBack);
        //LeanTween.moveLocalY(container, -300, 1.5f).setDelay(0.5f).setEase(LeanTweenType.easeInOutBack);
    }

    private void Resume() {
        Time.timeScale = 1;
        SoundManager.instance.ToggleMusic(false);
        //LeanTween.moveLocalY(pauseText, 200, 1.5f).setDelay(0.5f).setEase(LeanTweenType.easeInOutBack);
        //LeanTween.moveLocalY(container, 650, 1.5f).setEase(LeanTweenType.easeInOutBack);
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
}
