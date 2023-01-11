using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    public int health;
    public GameObject sceneTransform;//计分
    public float Speed;
    private Rigidbody2D rb;
    public float leftDistance, rightDistance;
    public float DieTime;
    public int score;
    private bool HaveTaken = false;
    private Transform playertransform;//玩家位置
    public AudioSource deathaudio;//死亡音效
    private bool isdied = false;
    void Start()
    {
        playertransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
     void Update()
    {
        Isdie();
    }

    public void TakeDamage(int damage)
    {
        if (health - damage >= 0)
        {
            health -= damage;
        }
        else
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
            Invoke("Killer", DieTime);//延时
            HaveTaken = true;
        }
    }
    void Killer()
    {
        Destroy(gameObject);
    }

}
