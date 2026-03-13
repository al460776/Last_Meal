using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] prefabs;
    public GameManager gameManager;

    public float spawnInterval = 5f;

    public float spTerval = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoints[1].SetActive(false);
        spawnPoints[3].SetActive(false);
        spawnPoints[5].SetActive(false);
        spawnPoints[7].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver == false)
        {
            if (gameManager.time >= 60f)
            {
                spawnPoints[1].SetActive(true);
                spawnPoints[3].SetActive(true);
                spawnPoints[5].SetActive(true);
                spawnPoints[7].SetActive(true);
            }
            /*
            if (gameManager.time == 30f || gameManager.time == 120f || gameManager.time == 240f || gameManager.time == 360f || gameManager.time == 480f || gameManager.time == 600f)
            {
                spTerval -= 1f;
            }
    */

            if (gameManager.time >= spawnInterval)
            {
                Spawn();
                spawnInterval += spTerval;
            }
        }
    }
    private void Spawn()
    {
        if (gameManager.isGameOver == false)
        {
            for (int i = spawnPoints.Length -1 ; i >= 0; i--)
            {
                if (spawnPoints[i].activeSelf == true)
                {
                    int prefabIndex = Random.Range(0, 5);
                    //Debug.Log("Prefab Index: " + prefabIndex);

                    if (prefabIndex == 0)
                    {
                        GameObject npc =Instantiate(prefabs[1], spawnPoints[i].transform.position, Quaternion.identity);
                        npc.GetComponent<NpcMovement>().gameManager = gameManager;
                    }
                    else if (prefabIndex == 1 || prefabIndex == 2 || prefabIndex == 3)
                    {
                        GameObject npc2 =Instantiate(prefabs[0], spawnPoints[i].transform.position, Quaternion.identity);
                        npc2.GetComponent<NpcMovement>().gameManager = gameManager;
                    }   
                }
            }   
        }
    }
}
