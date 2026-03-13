using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float time = 0f;
    public int minuts = 0;
    public int seconds = 0;
    public bool isPaused = false;
    public bool isGameOver = false;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    public ContractedSpawn contractedSpawn;
    public int countVillagers = 0;
    public GameObject contractedPanel;

    public Slider healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       healthBar.value = 1f;
       scoreText.text = "Score: " + score;
       gameOverPanel.SetActive(false);
       contractedPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == false)
        {
            time += Time.deltaTime;
            minuts = Mathf.FloorToInt(time / 60);
            seconds = Mathf.FloorToInt(time % 60);
        }

        if (countVillagers >= 10)
        {
            contractedPanel.SetActive(true);
        }
        else
        {
            contractedPanel.SetActive(false);
        }


        //Debug.Log("Time: " + minuts + ":" + seconds);
    }

    public void ScorePointsRestaurant(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Villager"))
        {
            score += 5;
            Destroy(collision.gameObject);
            countVillagers++;
            UpdateScoreText();
        }

        if (collision.gameObject.CompareTag("Thief"))
        {
            score -= 3;
            Destroy(collision.gameObject);
            UpdateScoreText();
            SubstractLive();
        }
    }

    public void ScorePointsPlayer(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Villager"))
        {
            score -= 5;
            Destroy(collision.gameObject);
            UpdateScoreText();
        }

        if (collision.gameObject.CompareTag("Thief"))
        {
            score += 3;
            Destroy(collision.gameObject);
            UpdateScoreText();
        }
    }

    public void UpdateScoreText()
    {
        
        scoreText.text = "Score: " + score;
    }
    public void SubstractLive()
    {
        healthBar.value -= 0.2f;
        if (healthBar.value <= 0)
        {
            GameOver();
        }
    }

    public void ContracetHability()
    {
        if (countVillagers >= 10)
        {
            countVillagers = 0;
            contractedSpawn.SpawnContractedVillager();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        scoreText.gameObject.SetActive(false);
        gameOverText.text = "Game Over!\n Score: " + score + "\nTime: " + minuts + ":" + seconds + "\nFinal Score: " + score * (int)Math.Round(time) / 2;
        score = score * (int)Math.Round(time) / 2;
        Debug.Log("Game Over!");
        SaveScore();
    }

    
    public void SaveScore()
    {
        ScoreManage scoreManage = GetComponent<ScoreManage>();
        scoreManage.WriteScore();
    }
    
}
