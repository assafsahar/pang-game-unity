using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private float forceX, forceY;

    private Rigidbody2D ballBody;

    [SerializeField]
    private bool moveLeft, moveRight;

    [SerializeField]
    private GameObject originalBall;
    private GameObject ball1, ball2;

    private BallScript ballScript1, ballScript2;

    private void Awake()
    {
        ballBody = GetComponent<Rigidbody2D>();
        SetBallSpeed();
    }

    private void FixedUpdate()
    {
        MoveBall();
    }

    private void MoveBall()
    {
        if(moveLeft){
            Vector3 tempPosition = transform.position;
            tempPosition.x -= forceX * Time.deltaTime;
            transform.position = tempPosition;
        }
        if(moveRight){
            Vector3 tempPosition = transform.position;
            tempPosition.x += forceX * Time.deltaTime;
            transform.position = tempPosition;
        }
    }

    private void SetBallSpeed()
    {
        forceX = 2.5f;
        // each ball prefab has a different tag
        // tha affects its forceY
        switch(this.gameObject.tag)
        {
            case "BallBig":
            forceY = 11f;
            break;
            case "BallMedium":
            forceY = 9.5f;
            break;
            case "BallSmall":
            forceY = 8.5f;
            break;
            case "BallTiny":
            forceY = 7.5f;
            break;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        // colliding with the ground make it bounce up
        if(target.tag == "Ground")
        {
            ballBody.velocity = new Vector2(0,forceY);
        }
        // colliding with the right wall make it go left
        if(target.tag == "RightWall")
        {
            SetMoveLeft(true);
        }
        // colliding with the left wall make it go right
        if(target.tag == "LeftWall")
        {
            SetMoveRight(true);
        }
        // colliding with a spear make it disappear
        if(target.tag == "Spear")
        {
            InstantiateNewBalls();
            gameObject.SetActive(false);
        }
    }

    private void InstantiateNewBalls()
    {
        // except for the smallest ball
        // all others spawn 2 smaller balls
        // after colliding with a spear
        if(this.tag != "BallTiny")
        {
            ball1 = Instantiate(originalBall, transform.position, transform.rotation);
            ball1.name = originalBall.name;
            ball1.GetComponent<AudioSource>().Play();
            ballScript1 = ball1.GetComponent<BallScript>();
            ballScript1.SetMoveLeft(true);
            // ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0,2.5f);
            ball2 = Instantiate(originalBall, transform.position, transform.rotation);
            ball2.name = originalBall.name;
            ballScript2 = ball2.GetComponent<BallScript>();
            ballScript2.SetMoveRight(true);
        }
        
    }

    private void SetMoveLeft(bool canMoveLeft)
    {
        this.moveLeft = canMoveLeft;
        this.moveRight = !canMoveLeft;
    }
    private void SetMoveRight(bool canMoveRight)
    {
        this.moveLeft = !canMoveRight;
        this.moveRight = canMoveRight;
    }
}
