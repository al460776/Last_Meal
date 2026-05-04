using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public Animator animator;
    public float speed = 2f;
    Vector3 target = Vector3.zero;

    public bool villager = true;

    public GameManager gameManager;

    public float stopDuration = 0.5f;
    private float stopTimer = 0f;
    public bool isStopped = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (GameManager.mood)
        {
            animator.SetBool("Devil", true);
        }
    }
/*
    void Start()
    {
        animator = GetComponent<Animator>();
    }
*/

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver == false && isStopped == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if (gameManager.isPartnerActive && tag == "Villager")
        {
            GetComponent<SpriteRenderer>().sprite = gameManager.spritePartner;
        }
        else if (tag == "Villager")
        {
            GetComponent<SpriteRenderer>().sprite = gameManager.spriteDefault;
        }

        if (isStopped)
        {
            if (!GameManager.mood)
            {
                if (villager)
                {
                    animator.SetBool("VillagerStop", true);
                }
                else
                {
                    animator.SetBool("ThiefStop", true);
                }
                stopTimer += Time.deltaTime;
                if (stopTimer >= stopDuration)
                {
                    isStopped = false;
                    stopTimer = 0f;
                    Destroy(gameObject);
                }
            }
            else
            {
                if (villager)
                {
                    animator.SetBool("DevilStop", true);
                }
                else
                {
                    animator.SetBool("DevilStop", true);
                }
                stopTimer += Time.deltaTime;
                if (stopTimer >= stopDuration)
                {
                    isStopped = false;
                    stopTimer = 0f;
                    Destroy(gameObject);
                } 
            }
            
        }

        if (gameManager.isPartnerActive)
        {
            if (GameManager.mood)
            {
                if (!villager)
                {
                    animator.SetBool("DevilPartner", true);
                }
            }
            else
            {
                if (villager)
                {
                    animator.SetBool("Partner", true);
                }
            }
        }
        else
        {
            if (GameManager.mood)
            {
                if (!villager)
                {
                    animator.SetBool("DevilPartner", false);
                }
            }
            else
            {
                if (villager)
                {
                    animator.SetBool("Partner", false);
                }
            }
        }
    }
}
