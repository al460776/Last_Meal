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
    //Falta hacer dos counts tanto de Villagers como Thieves pa datos, este solo pa habilidades.
    public TextMeshProUGUI contractedPanelText;
    public GameObject contractedHabText;
    public GameObject partnerHabText;

    public GameObject contractedPanel;
    public GameObject partnerPanel2;
    public float partnerDuration = 10f;
    public bool isPartnerActive = false;
    public PlayerController playerController;


    public Slider healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       healthBar.value = 1f;
       scoreText.text = "Score: " + score;
       gameOverPanel.SetActive(false);
       contractedPanel.SetActive(false);
       partnerPanel2.SetActive(false);
       contractedHabText.SetActive(false);
       partnerHabText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        contractedPanelText.text = countVillagers.ToString();
        if (isGameOver == false)
        {
            time += Time.deltaTime;
            minuts = Mathf.FloorToInt(time / 60);
            seconds = Mathf.FloorToInt(time % 60);
        }

        if (countVillagers >= 10)
        {
            contractedPanel.SetActive(true);
            contractedHabText.SetActive(true);
        }
        else
        {
            contractedPanel.SetActive(false);
            contractedHabText.SetActive(false);
        }

        if (countVillagers >= 20)
        {
            partnerPanel2.SetActive(true);
            partnerHabText.SetActive(true);
        }
        else
        {
            partnerPanel2.SetActive(false);
            partnerHabText.SetActive(false);
        }

        if (isPartnerActive)
        {
            partnerDuration -= Time.deltaTime;
            if (partnerDuration <= 0)
            {
                isPartnerActive = false;
                partnerDuration = 10f;
                playerController.triggerCollider.isTrigger = false;
                playerController.triggerSize = new Vector2(1f, 1f);
            }
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
            //Futuro, linea dejar en solo = 0, o dejarlo así
            countVillagers -= 10;
            contractedSpawn.SpawnContractedVillager();
        }
    }

    public void PartnerHability()
    {
        if (countVillagers >= 20)
        {
            //Futuro, linea dejar en solo = 0, o dejarlo así
            countVillagers -= 20;
            isPartnerActive = true;
            playerController.triggerSize = new Vector2(5f, 5f);
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
