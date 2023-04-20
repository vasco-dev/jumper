using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject MainMenu;

    [SerializeField] private GameObject TutorialUI;

    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject GameOverUI;

    [SerializeField] private TextMeshProUGUI TimerText;
    private float maxTime = 60;
    private float currentTime;
    private StringBuilder timer = new StringBuilder();

    [SerializeField] private TextMeshProUGUI ScoreText;
    //variable for internally storing player score
    private int playerScore;
    //the score that shall be presented in the UI
    private StringBuilder score = new StringBuilder();

    [SerializeField] private TextMeshProUGUI HiScoreText;

    [SerializeField] private TextMeshProUGUI ScoreGameOver;
    [SerializeField] private TextMeshProUGUI HiScoreGameOver;

    private bool gameIsRunning;

    [SerializeField] private Animator FinalScoreAnimator;
    private int animationRate = 2;
    private int tempScore;
    private bool animateScore;

    private Vector3 respawnPoint;
    private Vector3 unstuck = new Vector3(0,0.75f, 0);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Multiple Game Managers!!!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetScore();
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

        if (animateScore)
        {
            if (tempScore < playerScore)
            {
                tempScore += animationRate;
                ScoreGameOver.text = tempScore.ToString();
            }
            else
            {
                ScoreGameOver.text = playerScore.ToString();
                if (ScoreGameOver.text == playerScore.ToString())
                {
                    animateScore = false;
                    //FinalScoreAnimator.ResetTrigger("ExitAnim");
                    FinalScoreAnimator.Play("FinalScore");
                }
                tempScore = 0;
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
            EndGame();
        }
    }

    public void StartGame()
    {
        respawnPoint = new Vector3(0, 0, 0);
        FinalScoreAnimator.SetBool("ExitAnim", true);
        animateScore = false;
        MainMenu.SetActive(false);
        GameOverUI.SetActive(false);
        GameUI.SetActive(true);
        currentTime = 0;
        ResetScore();
        UpdateHiScore();
        FindObjectOfType<BackgroundLerping>().ResetBackground();
        gameIsRunning = true;
        AudioManager.Instance.Play("Start");

        PlayerController.Instance.StartNew();
        PlatformManager.Instance.StartNew();
    }

    public void ShowTutorial()
    {
        gameIsRunning = false;
        TutorialUI.SetActive(true);
        GameUI.SetActive(false);
    }

    public void HideTutorial()
    {
        gameIsRunning = true;
        TutorialUI.SetActive(false);
        GameUI.SetActive(true);
    }

    public void EndGame()
    {
        AudioManager.Instance.Play("Gameover");
        FinalScoreAnimator.SetBool("ExitAnim", false);
        gameIsRunning = false;
        animateScore = true;
        GameUI.SetActive(false);
        GameOverUI.SetActive(true);
        ScoreGameOver.text = playerScore.ToString();
        if (playerScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
        }
        HiScoreGameOver.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void AddScore(int increment)
    {
        AudioManager.Instance.Play("Score");
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

    private void UpdateHiScore()
    {
        HiScoreText.text = "HiScore\n" + PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void SetRespawnPosition(Vector3 position)
    {
        respawnPoint = position + unstuck;
    }
}
