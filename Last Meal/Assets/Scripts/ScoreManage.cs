using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class ScoreData
{
    public int score;
    public int minutes;
    public int seconds;
}

public class ScoreManage : MonoBehaviour
{
    public GameManager manager;
    private List<ScoreData> allScores = new List<ScoreData>();

    public void WriteScore()
    {
        ScoreData data = new ScoreData();
        data.score = manager.score;
        data.minutes = manager.minuts;
        data.seconds = manager.seconds;

        string json = JsonUtility.ToJson(data, true);
        Debug.Log("Datos de telemetría en JSON: " + json);

        string fechaHora = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string nombreArchivo = $"dataGame_{fechaHora}.json";
        string ruta = Path.Combine(Application.persistentDataPath, nombreArchivo);
        Debug.Log("Ruta del archivo de telemetría: " + ruta);

        File.WriteAllText(ruta, json);
    }
}
