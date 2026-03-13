using UnityEngine;
using UnityEngine.UIElements;

public class ContractedCollision : MonoBehaviour
{
    public GameManager gameManager;
    public Vector2 triggerSize = new Vector2(5f, 5f);
    private BoxCollider2D boxCollider;
    private BoxCollider2D triggerCollider;

    public Vector3 targetPosition;
    public float speed = 2f;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        triggerCollider = GetComponent<BoxCollider2D>();
        triggerCollider.isTrigger = false;
        triggerCollider.size = triggerSize;
    }

    void Update()
    {
       if (targetPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.gameObject.CompareTag("Villager") || collision.gameObject.CompareTag("Player"))
        {
            triggerCollider.isTrigger = true;
        }   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Thief"))
        {
            triggerCollider.isTrigger = false;        
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Thief"))
        {
            gameManager.ScorePointsPlayer(collision);
            Destroy(gameObject);  
        }
        
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Villager") || other.gameObject.CompareTag("Player"))
        {
            triggerCollider.enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerSize.x / 2f);       
    }
}
