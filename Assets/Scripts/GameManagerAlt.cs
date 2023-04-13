using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class GameManagerAlt : MonoBehaviour
{
    //put in GameManager -> public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI timerText;
    private float maxTime = 60;
    private float currentTime;
    private StringBuilder timer = new StringBuilder();

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
        }
    }

    private void Timer()
    {
        if (maxTime > currentTime)
        {
            currentTime += Time.deltaTime;
            timer.Clear();
            timer.Append((int)maxTime - (int)currentTime);
            timerText.text = timer.ToString();
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
        gameIsRunning = true;
    }

    public void EndGame()
    {
        /*
         * gameIsRunning = false;
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        HighScoreGameOver.text = PlayerPrefs.GetInt("HighScore").ToString();
        */
    }
}
