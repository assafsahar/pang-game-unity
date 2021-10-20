using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour
{
    private Rigidbody2D spearBody;
    private float speed = 5f;

    void Awake()
    {
        spearBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        spearBody.velocity = new Vector2(0,speed);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Top")
        {
            // spear destroyed above the screen
            // in order to save memory
            Destroy(gameObject); 
        }
        if(target.tag.Contains("Ball"))
        {
            Destroy(gameObject);
        }
    }
}
