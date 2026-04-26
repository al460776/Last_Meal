using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameManager gameManager;
    public GameObject[] positions;

    public Vector2 triggerSize = new Vector2(0f, 0f);
    private BoxCollider2D boxCollider;
    public BoxCollider2D triggerCollider;

    public Vector3 targetPosition;
    public float speed = 2f;

    public float stopDuration = 0.5f;
    public float stopDuration2 = 2f;

    private float stopTimer = 0f;
    private float loseTime = 0f;
    private bool losingAnim = false;
    private bool endGame = false;

    private bool isStopped = false;

    //Mando
    private PlayerInput controls;
    private InputAction moveAction;
    private Vector2 moveInput;
    private InputAction contractAction;
    private InputAction partnerAction;
    void Awake()
    {
        controls = GetComponent<PlayerInput>();
        moveAction = controls.actions["Move"];
        contractAction = controls.actions["Contracted"];
        partnerAction = controls.actions["Partner"];          
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        triggerCollider = GetComponent<BoxCollider2D>();
        triggerCollider.isTrigger = false;
        triggerCollider.size = triggerSize;
        animator.SetBool("EvilLose", false);
        animator.SetBool("GodLose", false);
        animator.SetBool("EvilStop", false);
        animator.SetBool("GodStop", false);
    }

    void Update()
    {
        if (!endGame)
        {
           if (Input.GetKeyDown(KeyCode.Q) || contractAction.triggered)
            {
                gameManager.ContracetHability();
            }

            if (Input.GetKeyDown(KeyCode.E) || partnerAction.triggered)
            {
                gameManager.PartnerHability();
            }
            
            if (isStopped)
            {
                stopTimer += Time.deltaTime;
                if (stopTimer >= stopDuration)
                {
                    stopTimer = 0f;
                    isStopped = false;
                    animator.SetBool("GodStop", false);
                    animator.SetBool("EvilStop", false);
                }  
            } 

            if (losingAnim)
            {
                loseTime += Time.deltaTime;
                if (loseTime >= stopDuration2)
                {
                    loseTime = 0f;
                    losingAnim = false;
                    if (!endGame)
                    {
                        endGame = true;
                        gameManager.GameOver();
                    }
                }
            } 
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Opción 1
        if(gameManager.isGameOver == false)
        {
            Vector2 stick = moveAction.ReadValue<Vector2>();
            float ejeX = stick.x;
            float ejeY = stick.y;

            //Prueba mejora Zona muerta Mando
            float deadZo = 0.23f;
            float diagonales = 0.77f;

            float diagX = Mathf.Abs(ejeX);
            float diagY = Mathf.Abs(ejeY);

            bool isUp = ejeY > deadZo;
            bool isDown = ejeY < -deadZo;
            bool isLeft = ejeX < -deadZo;
            bool isRight = ejeX > deadZo;

            bool isDiagUpRight = isUp && isRight && Mathf.Abs(diagX - diagY) < diagonales;
            bool isDiagUpLeft = isUp && isLeft && Mathf.Abs(diagX - diagY) < diagonales;
            bool isDiagDownRight = isDown && isRight && Mathf.Abs(diagX - diagY) < diagonales;
            bool isDiagDownLeft = isDown && isLeft && Mathf.Abs(diagX - diagY) < diagonales;

             if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) || isDiagUpLeft)
            {
                transform.position = positions[4].transform.position;
                //Debug.Log("W and A pressed");
            }
            else if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) || isDiagUpRight)
            {
                transform.position = positions[5].transform.position;
                //Debug.Log("W and D pressed");
            }
            else if ((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) || isDiagDownLeft)
            {
                transform.position = positions[6].transform.position;
                //Debug.Log("S and A pressed");
            }
            else if ((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) || isDiagDownRight)
            {
                transform.position = positions[7].transform.position;
                //Debug.Log("S and D pressed");
            }else if (Input.GetKey(KeyCode.W) || isUp)
            {
                transform.position = positions[0].transform.position;
                //Debug.Log("W pressed");
            }
            else if (Input.GetKey(KeyCode.S) || isDown)
            {
                transform.position = positions[1].transform.position;
                //Debug.Log("S pressed");
            }
            else if (Input.GetKey(KeyCode.A) || isLeft)
            {
                transform.position = positions[2].transform.position;
                //Debug.Log("A pressed");
            }
            else if (Input.GetKey(KeyCode.D) || isRight)
            {
                transform.position = positions[3].transform.position;
                //Debug.Log("D pressed");
            }
        }       
    }

    public void Derrota()
    {
        if (!losingAnim)
        {
            losingAnim = true;
            loseTime = 0f;
            if (GameManager.mood)
            {
                animator.SetBool("EvilLose", true);
            }
            else
            {
                animator.SetBool("GodLose", true);
            }
        }
        //gameManager.GameOver();
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
                if (GameManager.mood)
                {
                    animator.SetBool("EvilStop", true);
                }
                else
                {
                    animator.SetBool("GodStop", true);
                }
                isStopped = true;

            }
        }
        else if(collision.gameObject.CompareTag("Thief") || collision.gameObject.CompareTag("Villager"))
        {
            gameManager.ScorePointsPlayer(collision);
            if (GameManager.mood)
                {
                    animator.SetBool("EvilStop", true);
                }
                else
                {
                    animator.SetBool("GodStop", true);
                }
            isStopped = true;
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
