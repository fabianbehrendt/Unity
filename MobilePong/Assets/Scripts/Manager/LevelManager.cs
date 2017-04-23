using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }


    public Text scoreLeftPlayerText;
    public Text scoreRightPlayerText;
    public GameObject roundInfoPanel;
    public GameObject pauseMenuPanel;
#if UNITY_STANDALONE
    public GameObject leftPlayer;
    public GameObject rightPlayer;
#endif
    public GameObject ball;


    private int roundWinner = -1;   // -1 --> no Winner yet, 1 --> Left Player, 2 --> Right Player
    private int gameWinner = -1;
    private int scoreLeftPlayer = 0;
    private int scoreRightPlayer = 0;
    private int endScore = 5;
    private int roundNumber = 0;
    private bool ballStopped = false;
#if UNITY_ANDROID || UNITY_IOS
    private TouchInputManager touchControls;
#endif


    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        if (pauseMenuPanel.activeSelf && !ballStopped)
        {
            ball.GetComponent<BallController>().StopBall();
            ballStopped = true;
        }
        else if (!pauseMenuPanel.activeSelf && ballStopped)
        {
            ball.GetComponent<BallController>().ResumeBall();
            ballStopped = false;
        }
    }


    private void Start()
    {
        SetScore();

#if UNITY_ANDROID || UNITY_IOS
        touchControls = GetComponent<TouchInputManager>();
#endif

        StartCoroutine(GameLoop());
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(BeginRound());

        yield return StartCoroutine(RoundPlaying());

        yield return StartCoroutine(EndRound());

        if (gameWinner == -1)
            StartCoroutine(GameLoop());
        else
            SceneManager.LoadScene("MainMenu");
    }


    private IEnumerator BeginRound()
    {
        roundInfoPanel.GetComponentInChildren<Text>().text = "ROUND " + ++roundNumber;
        roundInfoPanel.SetActive(true);

        roundWinner = -1;

        DisableControl();

        yield return new WaitForSeconds(3f);
    }


    private IEnumerator RoundPlaying()
    {
        roundInfoPanel.SetActive(false);

        EnableControl();

        while (roundWinner < 0)
        {
            yield return null;
        }
    }


    private IEnumerator EndRound()
    {

        string roundWinnerString = String.Empty;

        if (roundWinner == 1)
        {
            scoreLeftPlayer++;
            roundWinnerString = "LEFT";
        }
        else if (roundWinner == 2)
        {
            scoreRightPlayer++;
            roundWinnerString = "RIGHT";
        }

        roundInfoPanel.GetComponentInChildren<Text>().text = roundWinnerString + " PLAYER WON THE ROUND";

        if (scoreLeftPlayer >= 5 || scoreRightPlayer >= 5)
        {
            string gameWinnerString = String.Empty;

            if (scoreLeftPlayer >= endScore)
            {
                gameWinner = 1;
                gameWinnerString = "LEFT";
            }
            else if (scoreRightPlayer >= endScore)
            {
                gameWinner = 2;
                gameWinnerString = "RIGHT";
            }

            roundInfoPanel.GetComponentInChildren<Text>().text = "CONGRATULATIONS" + "\n\n" + gameWinnerString + " PLAYER WON THE GAME";
        }

        roundInfoPanel.SetActive(true);
        DisableControl();
        SetScore();

        yield return new WaitForSeconds(3f);
    }


    private void EnableControl()
    {
#if UNITY_STANDALONE
        leftPlayer.GetComponent<PlayerController>().enabled = true;
        rightPlayer.GetComponent<PlayerController>().enabled = true;
#elif UNITY_ANDROID || UNITY_IOS
        touchControls.enabled = true;
#endif

        ball.GetComponent<BallController>().enabled = true;
    }


    private void DisableControl()
    {
#if UNITY_STANDALONE
        leftPlayer.GetComponent<PlayerController>().enabled = false;
        rightPlayer.GetComponent<PlayerController>().enabled = false;
#elif UNITY_ANDROID || UNITY_IOS
        touchControls.enabled = false;
#endif

        ball.GetComponent<BallController>().enabled = false;
    }


    private void SetScore()
    {
        scoreLeftPlayerText.text = scoreLeftPlayer.ToString();
        scoreRightPlayerText.text = scoreRightPlayer.ToString();
    }


    public void SetRoundWinner(int winner)
    {
        roundWinner = winner;
    }
}
