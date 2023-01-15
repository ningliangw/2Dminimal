using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public AudioSource attackMusic;
    public float time;
    public float startTime;

    private Animator anim;
    private Animator attack;
    private BoxCollider2D coll2D;
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

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);//—” ±
        this.transform.tag = "enemies";
        coll2D.enabled = true;
        StartCoroutine(disabledHitbox());
    }
    IEnumerator disabledHitbox()
    {
        yield return new WaitForSeconds(time);//—” ±
        this.transform.tag = "Untagged";
        coll2D.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            StartCoroutine(StartAttack());
            attack.SetBool("attack", true);
            anim.SetBool("attack", true);
            attackMusic.Play();
        }
        
    }
}
