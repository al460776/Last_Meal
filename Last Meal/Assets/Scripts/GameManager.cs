using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;



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
    //Contador pa JSON y mostrar en pantalla
    public int countThievesEnter = 0;
    public int countVillagersEnter = 0;
    public int countVillagersBlock = 0;
    public int countThievesBlock = 0;

    public TextMeshProUGUI contractedPanelText;
    public GameObject contractedHabText;
    public GameObject partnerHabText;

    public GameObject contractedPanel;
    public GameObject partnerPanel2;
    public GameObject pausePanel;

    public float partnerDuration = 10f;
    public bool isPartnerActive = false;
    public PlayerController playerController;

    //Sprite pa cambiar
    public Sprite spriteDefault;
    public Sprite spritePartner;


    public Slider healthBar;
    //Points World
    private List<ScoreData> allScores = new List<ScoreData>();
    public static bool mood = false; //false = GOD, True = devil

    //Mando pausa
    public PlayerInput pauseAction;
    private InputAction pauseInputAction;

    //Botones
    public GameObject butonLocal;
    public GameObject butonFinal;

    void Awake()
    {
        string path = Application.persistentDataPath;
        string[] files = Directory.GetFiles(path, "dataGame_*.json");

        if (files.Length == 0)
        {
            Debug.LogWarning("No se encontraron archivos de telemetría.");
            return;
        }

        foreach (string file in files)
        {
            try
            {
                string json = File.ReadAllText(file);
                ScoreData data = JsonUtility.FromJson<ScoreData>(json);
                if (data != null)
                    allScores.Add(data);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error al leer {file}: {e.Message}");
            }
        }

        allScores = allScores.OrderByDescending(s => s.score).ToList();

        int countPoint = 0;
        for (int i = allScores.Count -1; i >= 0; i--)
        {
            if (allScores[i].score >= 0)
            {
                countPoint++;
                Debug.Log("Ha sumado");
            }
            else
            {
                countPoint--;
                Debug.Log("Ha restado");
            }
        }

        if (countPoint >= 0)
        {
            mood = false;
        }
        else
        {
            mood = true;
        }
        Debug.Log("Mood: " + mood + " CountPoint: " + countPoint);

        pauseInputAction = pauseAction.actions["Pause"];

    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
       healthBar.value = 1f;
       scoreText.text = "Score: " + score;
       gameOverPanel.SetActive(false);
       contractedPanel.SetActive(false);
       partnerPanel2.SetActive(false);
       contractedHabText.SetActive(false);
       partnerHabText.SetActive(false);
       //pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
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

            if (Input.GetKeyDown(KeyCode.Escape) || pauseInputAction.triggered && !isPaused)
            {
                Pause();
                isPaused = true;
            }else if (Input.GetKeyDown(KeyCode.Escape) || pauseInputAction.triggered && isPaused)
            {
                isPaused = false;
                Continue();
            }


        }
       
        //Debug.Log("Time: " + minuts + ":" + seconds);
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(butonLocal);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ScorePointsRestaurant(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Villager"))
        {
            score += 5;
            Destroy(collision.gameObject);
            countVillagers++;
            countVillagersEnter++;
            UpdateScoreText();
        }

        if (collision.gameObject.CompareTag("Thief"))
        {
            score -= 3;
            Destroy(collision.gameObject);
            countThievesEnter++;
            UpdateScoreText();
            SubstractLive();
        }
    }

    public void ScorePointsPlayer(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Villager"))
        {
            score -= 5;
            NpcMovement npc = collision.gameObject.GetComponent<NpcMovement>();
            npc.isStopped = true;
            countVillagersBlock++;
            UpdateScoreText();
        }

        if (collision.gameObject.CompareTag("Thief"))
        {
            score += 3;
            NpcMovement npc = collision.gameObject.GetComponent<NpcMovement>();
            npc.isStopped = true;
            countThievesBlock++;
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
            isGameOver = true;
            playerController.Derrota();
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
        //Quiero que si no se ha temrinado la animacion de muerte del jugador no mostrarlo.
        gameOverPanel.SetActive(true);
        scoreText.gameObject.SetActive(false);
        contractedPanel.SetActive(false);
        partnerPanel2.SetActive(false);
        contractedPanelText.gameObject.SetActive(false);
        contractedHabText.SetActive(false);
        partnerHabText.SetActive(false);
        int score2 = (score + countVillagersEnter + countThievesBlock - countThievesEnter - countVillagersBlock) * (int)Math.Round(time) / 2;
        gameOverText.text = "Game Over!\n\nScore: " + score + "\nTime: " + minuts + ":" + seconds + "\nVillager Enter: " + countVillagersEnter + "\nThief Enter: " + countThievesEnter + "\nVillager Block: " + countVillagersBlock + "\nThief Block: " + countThievesBlock + "\nFinal Score: " + score2;
        score = score2;
        Debug.Log("Game Over!");
        EventSystem.current.SetSelectedGameObject(butonFinal);
        SaveScore();
    }

    
    public void SaveScore()
    {
        ScoreManage scoreManage = GetComponent<ScoreManage>();
        scoreManage.WriteScore();
    }
    
}