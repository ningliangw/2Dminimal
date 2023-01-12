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
    public bool HaveTaken = false;

    private Rigidbody2D rb;
    private Transform playertransform;
    private playerHealth playerHealth;
    private bool isdied = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
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
}
