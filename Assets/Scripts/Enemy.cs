using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
<<<<<<< Updated upstream
    // Start is called before the first frame update
    protected Animator anim;
    public int health;
    public int damage;

    protected virtual void Start()
    {
=======
    private Animator anim;
    public GameObject sceneTransform;//�Ʒ�
    public AudioSource deathAudio;
    public int health;
    public int damage;
    public int score;
    public float DieTime;
    public bool HaveTaken = false;

    private Rigidbody2D rb;
    private Transform playertransform;
    private playerHealth playerHealth;
    private bool faceLeft = true;
    private bool isGround;
    private bool isdied = false;

     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
>>>>>>> Stashed changes
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();
        playertransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
    }

    // Update is called once per frame
    protected void Update()
    {
<<<<<<< Updated upstream
        if (health <= 0)
=======
        /*CheckGrounded();
        SwitchAnim();*/
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
>>>>>>> Stashed changes
        {
            Destroy(gameObject);
        }
    }
<<<<<<< Updated upstream

    protected void TakeDamage(int damage)
=======
    void Isdie()
    {
        if (health <= 0 && !HaveTaken)
            {
            rb.velocity = new Vector2(0, 0);
                anim.SetTrigger("die");
                deathAudio.Play();
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
>>>>>>> Stashed changes
    {
        health -= damage;
        GameController.camShake.Shake();
    }
}
