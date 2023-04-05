using System;
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

    public Ball ball {get; private set;}
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        LoadLevel(1);
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

    public void UpdateLives(int changeInLives)
    {
        lives += changeInLives;

        livesText.text = "Lives: " + lives;
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
        this.lives--;
        
        if (this.lives > 0)
        {
            ResetLevel();
        }
        else 
        {
            GameOver();
        }
    }



    public void GameOver()
    {
        this.ball.gameOver = false;
        gameOverPanel.SetActive(true);
        NewGame();
    }

    public void Hit(Brick brick)
    {
        this.score += brick.points;

        if (Cleared())
        {
            LoadLevel(this.level + 1);
        }
    }

    private bool Cleared()
    {
        for(int i =0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
            {
                return false;
            }
        }

        return true;
    }

}
