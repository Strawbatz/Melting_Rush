using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] public GameObject container;
    [SerializeField] InputActionReference escape;

    private void Awake() {
        escape.action.performed += ToggleMenu;
        container.SetActive(false);
    }

    private void ToggleMenu(InputAction.CallbackContext ctx) {
        container.SetActive(!container.activeSelf);
        if(container.activeSelf) {
            Pause();
        } else {
            Resume();
        }
    }

    private void Pause() {
        Time.timeScale = 0;
    }

    private void Resume() {
        Time.timeScale = 1;
    }

    private void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit() {
        Application.Quit();
    }
}
