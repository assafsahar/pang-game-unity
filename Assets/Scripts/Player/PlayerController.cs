using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PangInput pangInput;
    [SerializeField]
    private GameObject spear;
    private bool canShoot = true;
    [SerializeField]
    private AudioSource shootSound;

    private Rigidbody2D rb;
    private float movementX;
    private float speed = 10;
    private GameObject player;

void Awake(){
    pangInput = new PangInput();
}
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pangInput.Player.Fire.performed += ctx => {
            StartCoroutine("Shoot");
            shootSound.Play();
        };
    }


    IEnumerator Shoot(){
        if(canShoot){
            canShoot = false;
            Vector3 tempPosition = transform.position;
            tempPosition.y += 1.5f;
            Instantiate(spear,tempPosition,transform.rotation);
            yield return new WaitForSeconds(0.5f);
            canShoot = true;
        }
    }

    void OnEnable(){
        pangInput.Enable();
    }
    void OnDisable(){
        pangInput.Disable();
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        
        if(movementX > 0){
            Vector3 scale = transform.localScale;
            scale.x = 0.4f; // face right
            transform.localScale = scale;
        }
        else if(movementX < 0){
            Vector3 scale = transform.localScale;
            scale.x = -0.4f; // face left
            transform.localScale = scale;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX,0,0);
        rb.AddForce(movement * speed);

    }
}
