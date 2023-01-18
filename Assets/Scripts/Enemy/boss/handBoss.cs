using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class handBoss : MonoBehaviour
{
    public float distancenow;//距离
    public AudioSource attackMusic;//音效
    public float waitTime;
    public float startTime;
    public float temptime;
    private GameObject child1, child0,child2, child3, child4;
    private bool haveTaken = false;
    private Transform playertransform;//player的坐标
    void Start()
    {
        child0 = transform.GetChild(0).gameObject;
        child1 = transform.GetChild(1).gameObject;
        child2 = transform.GetChild(2).gameObject;
        child3 = transform.GetChild(3).gameObject;
        child4 = transform.GetChild(4).gameObject;
       playertransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
    }
    void Update()
    {
        if (playertransform != null && !haveTaken)
        {
            float distance = Mathf.Abs(transform.position.x - playertransform.position.x);//玩家和敌人的距离。为一个
            if (distance <= distancenow && transform.localPosition.y - playertransform.localPosition.y >=0)//玩家进入巡逻半径
            {
                haveTaken = true;
                StartCoroutine(Round2());
                attackMusic.Play();
            }
        }
    }

    IEnumerator Round2()
    {
        for (int q = 40; q > 0; q--)
        {//进入范围->1->左wait->23->中wait->45->右wait->左

            yield return new WaitForSeconds(startTime);
            transform.GetChild(0).gameObject.SetActive(true);
            this.transform.localPosition = new Vector3(703, this.transform.localPosition.y, this.transform.localPosition.z);
            yield return new WaitForSeconds(waitTime);
            this.transform.localPosition = new Vector3(793, this.transform.localPosition.y, this.transform.localPosition.z);
            child1.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            child2.SetActive(true);
            this.transform.localPosition = new Vector3(751, this.transform.localPosition.y, this.transform.localPosition.z);
            yield return new WaitForSeconds(waitTime);
            this.transform.localPosition = new Vector3(793, this.transform.localPosition.y, this.transform.localPosition.z);
            child3.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            child4.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            for (int i = 0; i < 5; i++)
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
