using System.Collections;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    public int health;
    public GameObject sceneTransform;//�Ʒ�
    private Rigidbody2D rb;
    public float DieTime;
    public int score;
    public bool HaveTaken = false;
    private Transform playertransform;
    public AudioSource deathaudio;
    private bool isdied = false;
    public int damage;

         void Start()
    {
        playertransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
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
                deathaudio.Play();
                player x = GameObject.FindGameObjectWithTag("player").GetComponent<player>();
                x.collectionsget += score;
                int y = x.collectionsget;
                if (y > 27)
                {
                    //            sceneTransform.GetComponent<scenetransform>().enabled = true;
                }
                Invoke("Killer", DieTime);//
                HaveTaken = true;
            }
    }
    void Killer()
    {
        Destroy(gameObject);
    }
}
