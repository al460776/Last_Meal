using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] positions;

    public Vector2 triggerSize = new Vector2(0f, 0f);
    private BoxCollider2D boxCollider;
    public BoxCollider2D triggerCollider;

    public Vector3 targetPosition;
    public float speed = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        triggerCollider = GetComponent<BoxCollider2D>();
        triggerCollider.isTrigger = false;
        triggerCollider.size = triggerSize;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameManager.ContracetHability();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            gameManager.PartnerHability();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Opción 1
        if(gameManager.isGameOver == false)
        {
             if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            transform.position = positions[4].transform.position;
            //Debug.Log("W and A pressed");
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            transform.position = positions[5].transform.position;
            //Debug.Log("W and D pressed");
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            transform.position = positions[6].transform.position;
            //Debug.Log("S and A pressed");
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            transform.position = positions[7].transform.position;
            //Debug.Log("S and D pressed");
        }else if (Input.GetKey(KeyCode.W))
        {
            transform.position = positions[0].transform.position;
            //Debug.Log("W pressed");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = positions[1].transform.position;
            //Debug.Log("S pressed");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = positions[2].transform.position;
            //Debug.Log("A pressed");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = positions[3].transform.position;
            //Debug.Log("D pressed");
        }
        }        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameManager.isPartnerActive)
        {
            if(collision.gameObject.CompareTag("Villager"))
            {
                triggerCollider.isTrigger = true;
            }
            else
            {
                gameManager.ScorePointsPlayer(collision);
            }
        }
        else
        {
            gameManager.ScorePointsPlayer(collision);
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
