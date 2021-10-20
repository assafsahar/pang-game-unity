using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Animator anim;

    [SerializeField] 
    private AudioSource failSound;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

// after hittin g a ball, the player position
// is changed to out of the screen
// until the game starts again
    private IEnumerator DieAndRestart()
    {
        failSound.Play();
        transform.position = new Vector3(0,500,0);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag.Contains("Ball"))
        {
            StartCoroutine("DieAndRestart");
        }
    }
}
