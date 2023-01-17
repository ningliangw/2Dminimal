using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossAttack : MonoBehaviour
{
    private PolygonCollider2D coll2D;
    public AudioSource attackAudio;
    public float distancenow;//距离
    private Transform playertransform;//player的坐标
    public bool haveTaken = false;
    public float Time;
    public int times;
    void Start()
    {

        coll2D = GetComponent<PolygonCollider2D>();
        playertransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
    }
    void Update()
    {
        if (playertransform != null && !haveTaken&&this.tag=="traiy")
        {
            float distance = Mathf.Abs(transform.position.x - playertransform.position.x);//玩家和敌人的距离。为一个
            if (distance <= distancenow)//玩家进入巡逻
            {
                haveTaken = true;
                attackAudio.Play();
                StartCoroutine(Active());
            }
        }
    }

    IEnumerator Active()
    {
        for (int i = 1; i <= times; i++)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(Time);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void Collopen()
    {
        playertransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        coll2D.enabled = true;
    }
    void close()
    {
        this.gameObject.SetActive(false);
    }
    void Collclose()
    {
        coll2D.enabled = false;
    }
}
