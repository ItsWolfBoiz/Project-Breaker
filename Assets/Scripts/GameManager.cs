using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int score = 0;
    public int lives = 3;
    public Text livesText;
    public Text scoreText;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject startGamePanel;
    public bool win;
    public bool gameOver;
    private bool gameStarted;
    public AudioClip gameOverClip;
    public AudioClip buttonClickedClip;

    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }

    public static GameManager Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            gameStarted = true;
            StartGame();
            LoadLevel(1);
            startGamePanel.SetActive(false);
        }
    }

    private void StartGame()
    {
        if (ball != null)
        {
            ball.ResetBall();
        }
        if (paddle != null)
        {
            paddle.ResetPaddle();
        }
    }

    private void LoadLevel(int level)
    {
        this.level = level;
        SceneManager.LoadScene("Level" + level);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();
    }

    public void UpdateLives(int livesChange)
    {
        lives += livesChange;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
            ball.GetComponent<Rigidbody2D>().Sleep();
        }
    }

    public void UpdateScore(int points)
    {
        score += points;

        scoreText.text = "Score: " + score;
    }


    public void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    public void Miss()
    {
        lives--;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
            AudioSource.PlayClipAtPoint(gameOverClip, Camera.main.transform.position);
        }
        else
        {
            ball.ResetBall();
            paddle.ResetPaddle();
        }
    }

    public void PlayAgain()
    {
        Restart();
        StartCoroutine(PlayButtonClickSound());
    }
    private IEnumerator PlayButtonClickSound()
    {
        yield return new WaitForSeconds(0.1f);
        AudioSource.PlayClipAtPoint(buttonClickedClip, transform.position);
    }

    public void Quit()
    {
        StartCoroutine(PlayButtonClickSound());
        Application.Quit();
        Debug.Log("Game Quit");

    }

    public void Hit(Brick brick)
    {
        score += brick.points;
        scoreText.text = "Score: " + score;

        if (CheckWin())
        {
            win = true;
            winPanel.SetActive(true);
            ball.GetComponent<Rigidbody2D>().Sleep();
        }

    }

    public bool CheckWin()
    {
        foreach (Brick brick in bricks)
        {
            if (!brick.unbreakable && brick.gameObject.activeSelf)
            {
                return false;
            }
        }

        return true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level" + level);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        lives = 3;
        score = 0;
        gameStarted = false;
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        winPanel.SetActive(false);
    }

}
