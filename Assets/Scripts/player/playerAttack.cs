using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public int damage;
    public float time;
    public float startTime;
    public Transform target;

    private Animator anim;
    private PolygonCollider2D coll2D;
    public AudioSource attackMusic;
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("player").GetComponent<Animator>();
        coll2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    void Attack()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1f&& anim.GetBool("isattacking"))
        {
            anim.SetBool("isattacking", false);
        }
        if (Input.GetMouseButtonDown(0) && anim.GetBool("isattacking") == false)
        {
            anim.SetTrigger("attack");
            StartCoroutine(StartAttack());
            anim.SetBool("isattacking", true);
            attackMusic.Play();

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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemies"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
        if (other.gameObject.CompareTag("crycry"))
        {
            if (GameObject.Find("player").GetComponent<Transform>().localScale.x * other.transform.localScale.x < 0)//朝向一致
            {
                other.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}
