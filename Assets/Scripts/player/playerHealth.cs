using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class playerHealth : MonoBehaviour
{
    public GameObject health;
    public float DieTime;
    public int HP;
    public int Blinks;
    public float Time;
    private bool isdied = false;
    private int maxHP;
    private Renderer myRender;
    void Start()
    {
        HP= GameObject.Find("54321").GetComponent<transform>().hp;
        GameObject.Find("54321").SetActive(false);
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
            SoundMananger.instance.PlayerHurt();
            HP -= damage;
        }
        else
        {
            HP = 0;
        }
        if (HP <= 0)
        {
            isdied = true;
            health.SetActive(true);
            SoundMananger.instance.PlayerDeath();
            Invoke("Killer", DieTime);
            
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
        transform.position = GameObject.FindGameObjectWithTag("player").GetComponent<player>().respawnPosition;
        HP = maxHP;
        health.SetActive(false);
        SoundMananger.instance.PlayerResurrect();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //掉落死亡
        if (collision.gameObject.CompareTag("deathLine"))
        {
            Killer();
        }
    }
}