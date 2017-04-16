using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }


    public int Difficulty { get; set; }


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        Difficulty = PlayerPrefs.GetInt("Difficulty", 1);
    }
}
