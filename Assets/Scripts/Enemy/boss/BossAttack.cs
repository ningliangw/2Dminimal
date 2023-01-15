using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossAttack : MonoBehaviour
{
    // private Animator attack;
    public AudioSource attackMusic;
    public float time;
    private PolygonCollider2D coll2D;
    private bool haveTaken = false;
    private Transform playertransform;//playerµÄ×ø±ê
    void Start()
    {
        playertransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        coll2D = GetComponent<PolygonCollider2D>();
    }
    void Update()
    {

    }
    IEnumerator Waitfortime(float time)
    {
        yield return new WaitForSeconds(time);//ÑÓÊ±
    }
    void Collopen()
    {
        coll2D.enabled = true;
    }
    void Collclose()
    {
        coll2D.enabled = false;
    }
}
