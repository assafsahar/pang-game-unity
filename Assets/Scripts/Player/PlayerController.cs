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

    void Awake()
    {
        pangInput = new PangInput();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateShootCallback();
    }

    // dealing with 'Fire' input event (shoot a spear & play a sound)
    private void CreateShootCallback()
    {
        pangInput.Player.Fire.performed += ctx => { 
            StartCoroutine("Shoot");
            shootSound.Play();
        };
    }

    private IEnumerator Shoot()
    {
        if(canShoot)
        {
            // spawn a spear
            canShoot = false;
            Vector3 tempPosition = transform.position;
            tempPosition.y += 1.5f;
            Instantiate(spear,tempPosition,transform.rotation);
            // this prevents from continuous shooting
            yield return new WaitForSeconds(0.5f); 
            canShoot = true;
        }
    }

    private void OnEnable()
    {
        pangInput.Enable();
    }
    private void OnDisable()
    {
        pangInput.Disable();
    }

    // dealing with 'Move' input event (we only need the x axis)
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        
        if(movementX > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = 0.4f; // face right
            transform.localScale = scale;
        }
        else if(movementX < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -0.4f; // face left
            transform.localScale = scale;
        }
    }

    // actually move the player
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX,0,0);
        rb.AddForce(movement * speed);

    }
}
