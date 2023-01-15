using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttack : MonoBehaviour
{
    private Animator anim;
    private Animator attack;
    public float radius;//����
    public AudioSource attackMusic;
    public float time;
    public float startTime;
    private BoxCollider2D coll2D;
    private Transform playertransform;//player������
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
            float distance = Mathf.Abs(transform.position.x - playertransform.position.x);//��Һ͵��˵ľ��롣Ϊһ��
            if (distance <=radius&&attack.GetBool("attack")==false)//��ҽ���Ѳ�߰뾶
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
        yield return new WaitForSeconds(startTime);//��ʱ
        coll2D.enabled = true;
        StartCoroutine(disabledHitbox());
    }
    IEnumerator disabledHitbox()
    {
        yield return new WaitForSeconds(time);//��ʱ
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
