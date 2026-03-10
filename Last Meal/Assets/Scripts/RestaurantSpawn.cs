using UnityEngine;

public class RestaurantSpawn : MonoBehaviour
{
    public GameManager gameManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.ScorePointsRestaurant(collision);
    }
}
