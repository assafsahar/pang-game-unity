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
    // Start is called before the first frame update
    void Start()
    {
        initVariables();
        SetBallSpeed();
    }

    void initVariables(){
        ballBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        moveBall();
    }

    void moveBall(){
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

    void SetBallSpeed(){
        forceX = 2.5f;
// Debug.Log("tag="+this.gameObject.tag);
        switch(this.gameObject.tag){
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

    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == "Ground"){
            ballBody.velocity = new Vector2(0,forceY);
        }
        if(target.tag == "RightWall"){
            SetMoveLeft(true);
        }
        if(target.tag == "LeftWall"){
            SetMoveRight(true);
        }
        if(target.tag == "Spear"){
           InstantiateNewBalls();
        //    Destroy(gameObject); 
            
            gameObject.SetActive(false);
        }
    }

    void InstantiateNewBalls(){
        if(this.tag != "BallTiny"){
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

    public void SetMoveLeft(bool canMoveLeft){
        this.moveLeft = canMoveLeft;
        this.moveRight = !canMoveLeft;
    }
    public void SetMoveRight(bool canMoveRight){
        this.moveLeft = !canMoveRight;
        this.moveRight = canMoveRight;
    }
}
