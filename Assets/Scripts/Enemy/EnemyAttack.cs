using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttack : MonoBehaviour
{
    private Animator anim;
    private Animator attack;
    public float radius;//距离
    public AudioSource attackMusic;
    public float time;
    public float startTime;
    private BoxCollider2D coll2D;
    private Transform playertransform;//player的坐标
    void Start()
    {
        playertransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        attack = transform.parent.GetComponent<Animator>();
        coll2D = GetComponent<BoxCollider2D>();
    }
     void Update()
    {
        if (playertransform != null)
        {
            float distance = Mathf.Abs(transform.position.x - playertransform.position.x);//玩家和敌人的距离。为一个
            if (distance <=radius&&attack.GetBool("attack")==false)//玩家进入巡逻半径
            {
                attack.SetBool("attack", true);
                anim.SetBool("attack", true);
                StartCoroutine(StartAttack());
                attackMusic.Play();
            }
        }
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);//延时
        coll2D.enabled = true;
        StartCoroutine(disabledHitbox());
    }
    IEnumerator disabledHitbox()
    {
        yield return new WaitForSeconds(time);//延时
        coll2D.enabled = false;
    }
    public void StopAttack()
    {
        attack.SetBool("attack", false);
        anim.SetBool("attack", false);
    }
 /*   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            attack.SetBool("attack", true);
            anim.SetBool("attack", true);
            attackMusic.Play();
        }
    }*/
}
