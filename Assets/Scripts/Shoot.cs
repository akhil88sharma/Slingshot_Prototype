using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shoot : BaseScript           //inherited the base script
{
    Vector2 startPos, endPos, direction;  //touch start position, touch end position, swipe direction

    public float xForce, yForce, zForce;  //to control throw force in X, Y and Z direction
    private Boolean hit = false;
    public GameObject pauseMenuUI;
    public GameObject loc;

    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if you touch the screen
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;    //getting touch start position
        }

        //if you release your finger
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endPos = Input.GetTouch(0).position;      //getting finger release position
            direction = startPos - endPos;            //calculating swipe direction in 2D space

            //add force to the rigidbody in 3D space depending on the swipe direction and throw force
            rb.isKinematic = false;
            rb.AddForce((direction.x) * xForce, (direction.y) * yForce, (direction.y) * zForce );
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if stone hits the target
        if (collision.gameObject.CompareTag("Target"))
        {
            
            hit = true;
            if (hit)
            {
                pause();
            }

        }
        //if stone hits the wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            ResetBall();
        }
        //if stone hits the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            ResetBall();
        }
    }

    //reset the stone position
    private void ResetBall()
    {
        rb.isKinematic = true;
        gameObject.transform.position = loc.transform .position;
    }
    //pause the game
    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;             //stop the game time
    }
    //replay the game
    public void Replay()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;             //start the game time
        SpawnTarget();                   //spawn target on different location
        hit = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}