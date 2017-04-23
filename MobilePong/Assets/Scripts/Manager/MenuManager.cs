using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public Toggle toggleEasy;
    public Toggle toggleMedium;
    public Toggle toggleHard;

#if UNITY_STANDALONE
    public GameObject buttonQuitPrefab;
#endif


    private void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);

#if UNITY_STANDALONE
        Button buttonQuit = Instantiate(buttonQuitPrefab, mainMenu.transform).GetComponent<Button>();
        buttonQuit.onClick.AddListener(OnClickClose);
#endif

        switch (GameManager.Instance.Difficulty)
        {
            case 0:
                toggleEasy.isOn = true;
                toggleMedium.isOn = false;
                toggleHard.isOn = false;

                OnClickEasy();
                break;

            case 1:
                toggleEasy.isOn = false;
                toggleMedium.isOn = true;
                toggleHard.isOn = false;

                OnClickMedium();
                break;

            case 2:
                toggleEasy.isOn = false;
                toggleMedium.isOn = false;
                toggleHard.isOn = true;

                OnClickHard();
                break;
        }
    }


    public void OnClickPlay()
    {
        SceneManager.LoadScene("Game");
    }


    public void OnClickSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }


    public void OnClickBack()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }


    public void OnClickClose()
    {
        Application.Quit();
    }


    public void OnClickEasy()
    {
        toggleEasy.interactable = false;
        toggleMedium.interactable = true;
        toggleHard.interactable = true;

        PlayerPrefs.SetInt("Difficulty", 0);
        GameManager.Instance.Difficulty = 0;
    }

    public void OnClickMedium()
    {
        toggleEasy.interactable = true;
        toggleMedium.interactable = false;
        toggleHard.interactable = true;

        PlayerPrefs.SetInt("Difficulty", 1);
        GameManager.Instance.Difficulty = 1;
    }

    public void OnClickHard()
    {
        toggleEasy.interactable = true;
        toggleMedium.interactable = true;
        toggleHard.interactable = false;

        PlayerPrefs.SetInt("Difficulty", 2);
        GameManager.Instance.Difficulty = 2;
    }
}
