using System.Collections;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Animator anim;
   // public GameObject sceneTransform;//�Ʒ�
    public AudioSource deathAudio;
    public int health;
    public int damage;
    public int score;
    public float DieTime;
    public float flashTime;
    public float radius;//距离
    public bool HaveTaken = false;

    private Rigidbody2D rb;
    private Transform playerTransform;
    private playerHealth playerHealth;
    private SpriteRenderer sr;
    private Color originalColor;

    private bool isdied = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        playerTransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();
    }

    void Update()
    {
        Isdie();
    }
    public void TakeDamage(int damage)
    {
        GameController.camShake.Shake();
        if (health - damage >= 0)
        {
            health -= damage;
            FlashColor(flashTime);
        }
        else if (health <= 0 && !HaveTaken)
        {
            health = 0;
            isdied = true;
        }
    }
    void Isdie()
    {
        if (health <= 0 && !HaveTaken)
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetTrigger("die");
            deathAudio.Play();
            Invoke("Killer", DieTime);//
            HaveTaken = true;
            player x = GameObject.FindGameObjectWithTag("player").GetComponent<player>();
            x.collectionsget += score;
            int y = x.collectionsget;
            if (y > 27)
            {
                //            sceneTransform.GetComponent<scenetransform>().enabled = true;
            }
        }
    }
    void Killer()
    {
        Destroy(gameObject);
    }

    void FlashColor(float time)
    {
        sr.color = Color.red;//变红闪烁
        Invoke("ResetColor", time);//延时调用ResetColor函数
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }

    void Attack()
    {
        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;//玩家和敌人的距离。为一个?
            if (distance < radius)//玩家进入巡逻半径
            {
               /* cut = 0;
                if (anim.GetBool("wake") == false)
                {
                    StartCoroutine(StartWake());
                }
                if (Begin)
                {
                    transform.position = UnityEngine.Vector2.MoveTowards(transform.position, playertransform.position, speed * Time.deltaTime);//追击
                }
            }
            else
            {
                cut++;
                if (cut >= lenth)
                {
                    anim.SetBool("wake", false);
                    anim.SetBool("move", false);
                    Begin = false;*/
                }
            }
        }
    }
}
