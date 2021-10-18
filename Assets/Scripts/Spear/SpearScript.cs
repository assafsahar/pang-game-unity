using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour
{
    private Rigidbody2D spearBody;
    private float speed = 5f;

    void initVariables(){
        spearBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        initVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        //Debug.Log(spearBody.velocity.y);
        spearBody.velocity = new Vector2(0,speed);
    }

    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == "Top"){
            Destroy(gameObject);
        }
        if(target.tag.Contains("Ball")){
            Destroy(gameObject);
        }
    }
}
