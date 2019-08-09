using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;


    private void Awake()
    {
        HidePauseMenu();
    }


    private void Update()
    {
        //#if UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }/*
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 5)
        {
            ShowPauseMenu();
        }
#endif*/
    }


    private void ShowPauseMenu()
    {
        pauseMenuPanel.SetActive(true);
    }


    public void HidePauseMenu()
    {
        pauseMenuPanel.SetActive(false);
    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
