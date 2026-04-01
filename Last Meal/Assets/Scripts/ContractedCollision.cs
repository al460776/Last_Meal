using UnityEngine;
using UnityEngine.UIElements;

public class ContractedCollision : MonoBehaviour
{
    private Animator animator;
    public GameManager gameManager;
    public Vector2 triggerSize = new Vector2(5f, 5f);
    private BoxCollider2D boxCollider;
    private BoxCollider2D triggerCollider;

    public Vector3 targetPosition;
    public float speed = 2f;
    /*
    public float stopDuration = 0.6f;
    private float stopTimer = 0f;
    private bool isStopped = false;
    */
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        triggerCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        triggerCollider.isTrigger = false;
        triggerCollider.size = triggerSize;
    }

    void Update()
    {
       if (targetPosition != null)
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
/*
        if (isStopped)
        {
            stopTimer += Time.deltaTime;
            if (stopTimer >= stopDuration)
            {
                isStopped = false;
                AutoDestroy();
                stopTimer = 0f;
                animator.SetBool("ContractedStop", false);
            }
            
        }
        */
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
        //animator.SetBool("ContractedStop", true);
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
/*
    private void AutoDestroy()
    {
        Destroy(this.gameObject);
    }
    */
}
