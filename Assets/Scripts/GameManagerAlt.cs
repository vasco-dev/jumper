using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class GameManagerAlt : MonoBehaviour
{
    //put in GameManager -> public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI TimerText;
    private float maxTime = 60;
    private float currentTime;
    private StringBuilder timer = new StringBuilder();

    [SerializeField] private TextMeshProUGUI ScoreText;
    //variable for internally storing player score
    private int playerScore;
    //the score that shall be presented in the UI
    private StringBuilder score = new StringBuilder();

    [SerializeField] private TextMeshProUGUI HiScoreGameOver;

    private bool gameIsRunning;

    void Awake()
    {
        /*
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Multiple Game Managers!!!");
        }
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsRunning)
        {
            Timer();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AddScore(10);
            }
        }
    }

    private void Timer()
    {
        if (maxTime > currentTime)
        {
            currentTime += Time.deltaTime;
            timer.Clear();
            timer.Append((int)maxTime - (int)currentTime);
            TimerText.text = timer.ToString();
        }
        else
        {
            Debug.Log("GameOver");
            //end game;
        }
    }

    public void StartGame()
    {
        currentTime = 0;
        ResetScore();
        gameIsRunning = true;
    }

    public void EndGame()
    {
        gameIsRunning = false;
        if (playerScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
        }
        HiScoreGameOver.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void AddScore(int increment)
    {
        playerScore += increment;
        score.Remove(7, score.Length - 7);
        score.Append(playerScore);
        ScoreText.text = score.ToString();
    }

    private void ResetScore()
    {
        playerScore = 0;
        score.Clear();
        score.Append("Score").AppendLine().Append(playerScore);
        ScoreText.text = score.ToString();
    }
}
