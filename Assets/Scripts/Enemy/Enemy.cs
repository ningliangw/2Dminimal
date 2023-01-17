using System.Collections;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Animator anim;
   // public GameObject sceneTransform;
    public AudioSource deathAudio;
    public int health;
    public int damage;
    public int score;
    public float DieTime;
    public float flashTime;
    public bool HaveTaken = false;
    public float repel;
    private Rigidbody2D rb;
    private Transform playertransform;
    private playerHealth playerHealth;
    private SpriteRenderer sr;
    private Color originalColor;
    private bool isdied = false;
    public GameObject getCollection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        playertransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
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
            transform.GetComponent<FSM>().enabled = false;
            health = 0;
            isdied = true;
            Isdie();
        }
    }
    void Isdie()
    {
        if (health <= 0 && !HaveTaken)
        {
            if (getCollection != null)
            {
                getCollection.SetActive(true);
            }

            rb.velocity = new Vector2(0, 0);
            HaveTaken = true;
            anim.SetTrigger("die");
            //     deathAudio.Play();
            Invoke("Killer", DieTime);//
            player x = GameObject.FindGameObjectWithTag("player").GetComponent<player>();
            playerHealth z = GameObject.FindGameObjectWithTag("player").GetComponent<playerHealth>();
            z.HP = 3;
            GameObject.FindGameObjectWithTag("player").GetComponent<Animator>().SetTrigger("devour");
            GameObject.FindGameObjectWithTag("player").GetComponent<Animator>().SetBool("isdevouring", true);
            x.collectionsget += score;
            int y = x.collectionsget;
            if (y > 27)
            {
                //            sceneTransform.GetComponent<scenetransform>().enabled = true;
            }
            if (transform.GetChild(0) != null)
            {
                transform.GetChild(0).gameObject.SetActive(false);
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
    IEnumerator Waitfortime()
    {
        yield return new WaitForSeconds(this.GetComponentInParent<EnemyBoss>().waitTime);//延时
    }
    void WaitWait()
    {
    
    }
}
