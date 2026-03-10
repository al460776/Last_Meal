using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] positions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Opción 1
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
        }else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position = positions[0].transform.position;
            //Debug.Log("W pressed");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = positions[1].transform.position;
            //Debug.Log("S pressed");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = positions[2].transform.position;
            //Debug.Log("A pressed");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = positions[3].transform.position;
            //Debug.Log("D pressed");
        }
        

        //Opción 2
        /*
        bool w = Input.GetKey(KeyCode.W);
        bool s = Input.GetKey(KeyCode.S);
        bool a = Input.GetKey(KeyCode.A);
        bool d = Input.GetKey(KeyCode.D);

        if (w && a)
        {
            transform.position = positions[4].transform.position;
            Debug.Log("W and A pressed");
        }
        else if (w && d)
        {
            transform.position = positions[5].transform.position;
            Debug.Log("W and D pressed");
        }
        else if (s && a)
        {
            transform.position = positions[6].transform.position;
            Debug.Log("S and A pressed");
        }
        else if (s && d)
        {
            transform.position = positions[7].transform.position;
            Debug.Log("S and D pressed");
        }
        else if (w)
        {
            transform.position = positions[0].transform.position;
            Debug.Log("W pressed");
        }
        else if (s)
        {
            transform.position = positions[1].transform.position;
            Debug.Log("S pressed");
        }
        else if (a)
        {
            transform.position = positions[2].transform.position;
            Debug.Log("A pressed");
        }
        else if (d)
        {
            transform.position = positions[3].transform.position;
            Debug.Log("D pressed");
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.ScorePointsPlayer(collision);
    }
}
