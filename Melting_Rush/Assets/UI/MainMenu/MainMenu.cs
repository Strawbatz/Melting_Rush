using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject buttonContainer;
    [SerializeField] GameObject levelsContainer;
    private bool animationIsRunning;

    private void Start() {
        LeanTween.scale(levelsContainer, Vector3.zero, 0f);
        LeanTween.moveLocalY(levelsContainer, -800, 0f);
    }
    
    public void LoadLevel(int level) {
        SceneManager.LoadScene("Level_"+level);
    }

    public void LevelSelect() {
        if(!animationIsRunning) {
            animationIsRunning = true;

            StartCoroutine(WaitThenHide(buttonContainer, 1f));
            LeanTween.moveLocalX(buttonContainer, -1350, 1).setEase(LeanTweenType.easeOutExpo);

            levelsContainer.SetActive(true);
            LeanTween.scale(levelsContainer, new Vector3(1f, 1f, 1f), 1f).setEase(LeanTweenType.easeInOutExpo);
            LeanTween.moveLocalY(levelsContainer, -110, 1).setEase(LeanTweenType.easeOutExpo);
        }
    }

    public void GoToMainMenu() {
        if(!animationIsRunning) {
            animationIsRunning = true;

            buttonContainer.SetActive(true);
            LeanTween.moveLocalX(buttonContainer, 0, 1).setDelay(0.2f).setEase(LeanTweenType.easeOutExpo);

            StartCoroutine(WaitThenHide(levelsContainer, 0.5f));
            LeanTween.scale(levelsContainer, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutExpo);
            LeanTween.moveLocalY(levelsContainer, -800, 1).setEase(LeanTweenType.easeOutExpo);
        }
    }

    IEnumerator WaitThenHide(GameObject obj, float t) {
        yield return new WaitForSeconds(t);
        obj.SetActive(false);
        animationIsRunning = false;
    }

    public void Exit() {
        Application.Quit();
    }
}
