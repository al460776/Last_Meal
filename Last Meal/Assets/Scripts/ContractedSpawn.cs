using UnityEngine;

public class ContractedSpawn : MonoBehaviour
{
    public GameObject[] positions;
    public GameObject[] spawnPoints;
    public GameObject contractedVillager;

    public GameManager gameManager;

    public float speed = 2f;

    void Start()
    {
        positions[1].SetActive(false);
        positions[3].SetActive(false);
        positions[5].SetActive(false);
        positions[7].SetActive(false);
    }

    void Update()
    {
        
        if (gameManager.isGameOver == false)
        {
            if (gameManager.time >= 60f)
            {
                positions[1].SetActive(true);
                positions[3].SetActive(true);
                positions[5].SetActive(true);
                positions[7].SetActive(true);
            }
        }
        
    }



    public void SpawnContractedVillager()
    {
        if (gameManager.isGameOver == false)
        {
            for (int i = positions.Length -1 ; i >= 0; i--)
            {
                if (positions[i].activeSelf == true)
                {
                    GameObject npc3 =Instantiate(contractedVillager, spawnPoints[i].transform.position, Quaternion.identity);
                    npc3.GetComponent<ContractedCollision>().gameManager = gameManager;
                    npc3.GetComponent<ContractedCollision>().targetPosition = positions[i].transform.position;
                    npc3.GetComponent<ContractedCollision>().speed = speed;
                }
            }   
        }
    }
}
