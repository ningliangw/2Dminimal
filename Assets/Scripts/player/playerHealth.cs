using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public float DieTime;
    public int HP;
    public int Blinks;
    public float Time;
    private bool isdied = false;
    private int maxHP;
    private Renderer myRender;
    void Start()
    {
        maxHP = HP;
        myRender = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void DamagePlayer(int damage)
    {
        if (HP - damage >= 0)
        {
            HP -= damage;
        }
        else
        {
            HP = 0;
        }
        if (HP <= 0)
        {
            isdied = true;
            Invoke("Killer", DieTime);
            transform.position = GameObject.FindGameObjectWithTag("player").GetComponent<player>().respawnPosition;
            HP = maxHP;
        }
        else
        {
            BlinkPlayer(Blinks, Time);
            Animator x = GameObject.FindGameObjectWithTag("player").GetComponent<Animator>();
            x.SetBool("hurt", true);
        }
    }
    public void Recovey(int recovey)
    {

        if (HP + recovey <= maxHP)
        {
            HP += recovey;
        }
        else
        {
            HP = maxHP;
        }
    }
    void Killer()
    {
        Destroy(gameObject);
    }
    void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }
    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i = 0; i < numBlinks * 2; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
}