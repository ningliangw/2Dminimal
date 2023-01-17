using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class FinalBoss : MonoBehaviour
{
    public float distancenow;//距离
    public AudioSource attackMusic;//音效
    public float waitTime;
    public float startTime;
    public float tempTime;
    public float Time3;
    public float restTime;
    private bool haveTaken = false;
    private Transform playertransform;//player的坐标
    void Start()
    {
        playertransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
    }
    void Update()
    {
        if (playertransform != null && !haveTaken)
        {
            float distance = Mathf.Abs(transform.position.x - playertransform.position.x);//玩家和敌人的距离。为一个
            if (distance <= distancenow)//玩家进入巡逻半径
            {
                // attackMusic.Play();
                haveTaken = true;
                StartCoroutine(Round2());
            }
        }
    }

    IEnumerator Round2()
    {
        yield return new WaitForSeconds(startTime);
        for (int q = 40; q > 0; q--)
        {//进入范围->1->左wait->23->中wait->45->右wait->左
            for (int i = 0; i < 3; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 0; i < 3; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }//1
            yield return new WaitForSeconds(waitTime);
            for (int i = 3; i < 6; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 3; i < 6; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }//2
            yield return new WaitForSeconds(waitTime);
            for (int i = 6; i < 14; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(1f);
            for (int i = 6; i < 14; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 6; i < 20; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 6; i < 20; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }//3
            yield return new WaitForSeconds(waitTime);
            for (int i = 20; i < 28; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(1f);
            for (int i = 20; i < 28; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 20; i < 34; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 20; i < 34; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }//4
            yield return new WaitForSeconds(restTime);
            this.transform.localPosition = new Vector3(-129,-328, this.transform.localPosition.z);//下一阶段
            yield return new WaitForSeconds(waitTime);
            for (int i = 1; i <= 9; i++)
            {
                transform.GetChild(34).gameObject.SetActive(true);
                yield return new WaitForSeconds(waitTime);
                transform.GetChild(34).gameObject.SetActive(false);
                transform.GetChild(34).localPosition = new Vector3(transform.GetChild(34).localPosition.x, transform.GetChild(34).localPosition.y+12, transform.GetChild(34).localPosition.z);
            }
            transform.GetChild(34).localPosition = new Vector3(transform.GetChild(34).localPosition.x, transform.GetChild(34).localPosition.y - 12*9, transform.GetChild(34).localPosition.z);//1
            yield return new WaitForSeconds(waitTime);
            for (int i = 35; i <= 44; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 35; i <= 44; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                transform.GetChild(i).localPosition = new Vector3(transform.GetChild(i).localPosition.x-12, transform.GetChild(i).localPosition.y, transform.GetChild(i).localPosition.z);
            }
            for (int i = 36; i <= 45; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }//3
            yield return new WaitForSeconds(waitTime);
            for (int i = 35; i <= 45; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                transform.GetChild(i).localPosition = new Vector3(transform.GetChild(i).localPosition.x + 12, transform.GetChild(i).localPosition.y, transform.GetChild(i).localPosition.z);
            }
            transform.GetChild(45).localPosition = new Vector3(transform.GetChild(45).localPosition.x - 12, transform.GetChild(45).localPosition.y, transform.GetChild(45).localPosition.z);
            yield return new WaitForSeconds(waitTime);
            for (int i = 46; i <= 49; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitForSeconds(waitTime);
            }
            for (int i = 46; i <= 49; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }//4
            yield return new WaitForSeconds(restTime);
            this.transform.localPosition = new Vector3(-129, -346, this.transform.localPosition.z);//下一阶段
            yield return new WaitForSeconds(waitTime);
            this.transform.localPosition = new Vector3(-174, -306, this.transform.localPosition.z);
            yield return new WaitForSeconds(tempTime);
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
