using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject spear;
    private Rigidbody2D playerBody;
    private Animator anim;
    private float speed = 10f;
    private float maxVelocity = 4f;

    private bool canShoot = true;

    [SerializeField] 
    private AudioSource failSound, shootSound;
    
    // Start is called before the first frame update
    void Start()
    {
        initVariables();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("shoot");
    }

    void FixedUpdate(){
        playerWalk();
    }

    void initVariables(){
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    IEnumerator shoot(){
        if(Input.GetMouseButtonDown(0) && canShoot){
            canShoot = false;
            shootSound.Play();
            Vector3 tempPosition = transform.position;
            tempPosition.y += 1.5f;
            Instantiate(spear,tempPosition,transform.rotation);
            yield return new WaitForSeconds(0.5f);
            canShoot = true;
        }
        
    }
    
    void playerWalk(){
        var force = 0f;
        var velocity = Mathf.Abs(playerBody.velocity.x);

        float h = Input.GetAxis("Horizontal");
        if(h > 0){
            // moving right
            if(velocity < maxVelocity){
                force = speed;
            }
            Vector3 scale = transform.localScale;
            scale.x = 0.4f; // face right
            transform.localScale = scale;
        }
        else if(h < 0){
            // moving left
            if(velocity < maxVelocity){
                force = -speed;
            }
            Vector3 scale = transform.localScale;
            scale.x = -0.4f; // face left
            transform.localScale = scale;
        }
        playerBody.AddForce(new Vector2(force,0));
    }

    IEnumerator DieAndRestart(){
        failSound.Play();
        transform.position = new Vector3(0,500,0);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter2D(Collider2D target){
        if(target.tag.Contains("Ball")){
            StartCoroutine("DieAndRestart");
        }
    }
}
