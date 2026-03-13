using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public float speed = 2f;
    Vector3 target = Vector3.zero;

    public bool villager = true;

    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
}
