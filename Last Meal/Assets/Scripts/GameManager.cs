using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public float time = 0f;
    public int minuts = 0;
    public int seconds = 0;
    public bool isPaused = false;
    public bool isGameOver = false;
    public int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        minuts = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);

        //Debug.Log("Time: " + minuts + ":" + seconds);
    }

    public void ScorePointsRestaurant(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Villager"))
        {
            score += 5;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Thief"))
        {
            score -= 3;
            Destroy(collision.gameObject);
        }
    }

    public void ScorePointsPlayer(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Villager"))
        {
            score -= 5;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Thief"))
        {
            score += 3;
            Destroy(collision.gameObject);
        }
    }
}
