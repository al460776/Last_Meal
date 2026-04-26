using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class LoadScore : MonoBehaviour
{

    public int topCount = 5; //Cuantos muestra de ranking

    public TMP_Text upRankingText; //Referencia al TextMeshPro para mostrar el ranking positivo
    public TMP_Text downRankingText; //Referencia al TextMeshPro para mostrar el ranking negativo
    public TMP_Text upPanelText;
    public TMP_Text downPanelText;
    private List<ScoreData> allScores = new List<ScoreData>();

    public GameObject upRankingPanel;
    public GameObject downRankingPanel;
    public GameObject mainPanel;
    //Botones
    public GameObject buttonExitUp;
    public GameObject buttonExitDown;
    public GameObject buttonMain;

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
        else
        {
            upPanelText.text = $"{ "#"} { "Score",8} { "Time",6} { "VE",4} { "TE",4} { "VB",4} { "TB",4}\n";
        }


        for (int i = 0; i < count; i++)
        {
            upRankingText.text += $"{i + 1}. {allScores[i].score,8} {($"{allScores[i].minutes}:{allScores[i].seconds:00}"),6} {allScores[i].countVillagersEnter,4} {allScores[i].countThievesEnter,4} {allScores[i].countVillagersBlock,4} {allScores[i].countThievesBlock,4}\n";
        }

        Debug.Log($"Mostrando top {count} puntuaciones en HUD");
        EventSystem.current.SetSelectedGameObject(buttonExitUp);
        //
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
        }else
        {
            downPanelText.text = $"{ "#"} { "Score",8} { "Time",6} { "VE",4} { "TE",4} { "VB",4} { "TB",4}\n";
        }

        for (int i = 0; i < count; i++)
        {
            downRankingText.text += $"{i + 1}. {allScores[i].score,8} {($"{allScores[i].minutes}:{allScores[i].seconds:00}"),6} {allScores[i].countVillagersEnter,4} {allScores[i].countThievesEnter,4} {allScores[i].countVillagersBlock,4} {allScores[i].countThievesBlock,4}\n";
        }

        Debug.Log($"Mostrando top {count} puntuaciones en HUD");
        EventSystem.current.SetSelectedGameObject(buttonExitDown);
    }

    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        upRankingPanel.SetActive(false);
        downRankingPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(buttonMain);
    }
}
