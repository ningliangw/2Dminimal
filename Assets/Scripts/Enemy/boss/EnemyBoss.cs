using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBoss : MonoBehaviour
{
    public float distancenow;//距离
    public AudioSource attackMusic;//音效
    public float waitTime;
    public float startTime;
    public float temptime;
    private GameObject child1,child0;
    private bool haveTaken = false;
    private Transform playertransform;//player的坐标
    void Start()
    {
        child0 = transform.GetChild(0).gameObject;
        child1 = transform.GetChild(1).gameObject;
        playertransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
    }
     void Update()
    {
        if (playertransform != null&&!haveTaken)
        {
            float distance = Mathf.Abs(transform.position.x - playertransform.position.x);//玩家和敌人的距离。为一个
            if (distance <=distancenow)//玩家进入巡逻半径
            {
               // attackMusic.Play();
                haveTaken = true;
                   StartCoroutine(Round2());
            }
        }
    }
    void Round1()
    {
        child0.SetActive(true);
        child1.SetActive(true);
    }

    IEnumerator Round2()
    {
        for (int q = 40; q > 0; q--)
        {
            Round1();
            yield return new WaitForSeconds(waitTime);
            for (int i = 2; i < 9; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitForSeconds(waitTime);
            }
            for (int i = 0; i < 9; i++)
            {
                if (i > 1) { transform.GetChild(i).localPosition = new Vector3(transform.GetChild(i).localPosition.x - 6, transform.GetChild(i).localPosition.y, transform.GetChild(i).localPosition.z); }
                transform.GetChild(i).gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 8; i > 1; i--)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitForSeconds(waitTime);
            }
            for (int i = 2; i < 9; i++)
            {
                transform.GetChild(i).localPosition = new Vector3(transform.GetChild(i).localPosition.x + 6, transform.GetChild(i).localPosition.y, transform.GetChild(i).localPosition.z);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 9; i < 18; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitForSeconds(waitTime);
            }
            for (int i = 2; i < 18; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 2; i < 9; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 1; i < 17; i++)
            {
                transform.GetChild(18).gameObject.SetActive(true);
                transform.GetChild(18).localPosition = new Vector3(transform.GetChild(18).localPosition.x + 6, transform.GetChild(18).localPosition.y, transform.GetChild(18).localPosition.z);
                yield return new WaitForSeconds(waitTime);
                transform.GetChild(18).gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(waitTime);
            transform.GetChild(18).localPosition = new Vector3(transform.GetChild(18).localPosition.x - 16 * 6, transform.GetChild(18).localPosition.y, transform.GetChild(18).localPosition.z);
            for (int i = 1; i < 16; i++)
            {
                transform.GetChild(9).gameObject.SetActive(true);
                transform.GetChild(9).localPosition = new Vector3(transform.GetChild(9).localPosition.x - 6, transform.GetChild(9).localPosition.y, transform.GetChild(9).localPosition.z);
                yield return new WaitForSeconds(waitTime);
                transform.GetChild(9).gameObject.SetActive(false);
            }
            transform.GetChild(9).localPosition = new Vector3(transform.GetChild(9).localPosition.x + 90, transform.GetChild(9).localPosition.y, transform.GetChild(9).localPosition.z);
            for (int i = 0; i < 19; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(temptime);
        }
    }
    IEnumerator Waitfortime(float time)
    {
        yield return new WaitForSeconds(time);//延时
    }
    public void StopAttack()
    {
      //  attack.SetBool("attack", false);
    }
}
