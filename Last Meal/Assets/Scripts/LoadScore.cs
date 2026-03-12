using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class LoadScore : MonoBehaviour
{

    public int topCount = 5; //Cuantos muestra de ranking

    public TMP_Text upRankingText; //Referencia al TextMeshPro para mostrar el ranking positivo
    public TMP_Text downRankingText; //Referencia al TextMeshPro para mostrar el ranking negativo
    private List<ScoreData> allScores = new List<ScoreData>();

    public GameObject upRankingPanel;
    public GameObject downRankingPanel;
    public GameObject mainPanel;

    void Start()
    {
        mainPanel.SetActive(true);
        upRankingPanel.SetActive(false);
        downRankingPanel.SetActive(false);
    }

    void LoadUpScores()
    {
        allScores.Clear();
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
    }

    public void ShowUpScores()
    {
        LoadUpScores();
        if (upRankingText == null)
        {
            Debug.LogWarning("No hay referencia al TextMeshPro para mostrar el ranking.");
            return;
        }

        upRankingPanel.SetActive(true);
        mainPanel.SetActive(false);
        upRankingText.text = ""; // limpiar texto

        int count = Mathf.Min(topCount, allScores.Count);

        if (count == 0)
        {
            upRankingText.text = "Play to score.";
            return;
        }

        for (int i = 0; i < count; i++)
        {
            upRankingText.text += $"{i + 1}.  \t{allScores[i].score} \t{allScores[i].minutes} \t{allScores[i].seconds}\n";
        }

        Debug.Log($"Mostrando top {count} puntuaciones en HUD");
    }

    void LoadDownScores()
    {
        allScores.Clear();

        string path = Application.persistentDataPath;
        string[] files = Directory.GetFiles(path, "dataGame_*.json");

        if (files.Length == 0)
        {
            Debug.LogWarning("No se encontraron archivos de datos del juego.");
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

        allScores = allScores.OrderBy(s => s.score).ToList();
    }

    public void ShowDownScores()
    {
        LoadDownScores();
        if (downRankingText == null)
        {
            Debug.LogWarning("No hay referencia al TextMeshPro para mostrar el ranking.");
            return;
        }

        downRankingPanel.SetActive(true);
        mainPanel.SetActive(false);
        downRankingText.text = ""; // limpiar texto

        int count = Mathf.Min(topCount, allScores.Count);

        if (count == 0)
        {
            downRankingText.text = "Play to score.";
            return;
        }

        for (int i = 0; i < count; i++)
        {
            downRankingText.text += $"{i + 1}.  \t{allScores[i].score} \t{allScores[i].minutes} \t{allScores[i].seconds}\n";
        }

        Debug.Log($"Mostrando top {count} puntuaciones en HUD");
    }

    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        upRankingPanel.SetActive(false);
        downRankingPanel.SetActive(false);
    }
}
