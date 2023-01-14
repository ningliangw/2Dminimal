using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private Animator anim;
    private Animator attack;
    private BoxCollider2D coll2D;
    public AudioSource attackMusic;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attack = transform.parent.GetComponent<Animator>();
        coll2D = GetComponent<BoxCollider2D>();
    }
   
    public void StopAttack()
    {
        attack.SetBool("attack", false);
        anim.SetBool("attack", false);
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            attack.SetBool("attack", true);
            anim.SetBool("attack", true);
            attackMusic.Play();
        }
        
    }
}
