using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject buttonContainer;
    [SerializeField] GameObject levelsContainer;
    
    public void LoadLevel(int level) {
        SceneManager.LoadScene("lvl"+level);
    }

    public void LevelSelect() {
        buttonContainer.SetActive(false);
        levelsContainer.SetActive(true);
    }

    public void GoToMainMenu() {
        buttonContainer.SetActive(true);
        levelsContainer.SetActive(false);
    }

    public void Exit() {
        Application.Quit();
    }
}
