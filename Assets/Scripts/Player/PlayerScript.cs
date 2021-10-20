using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    
    private Animator anim;

    [SerializeField] 
    private AudioSource failSound;
    
    // Start is called before the first frame update
    void Start()
    {
        initVariables();
    }

    void FixedUpdate(){
        
    }

    void initVariables(){
        anim = GetComponent<Animator>();
    }

    public void playSound(AudioSource sound){
        sound.Play();
    }

    IEnumerator DieAndRestart(){
        playSound(failSound);
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
