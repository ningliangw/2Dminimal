using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class FinalBoss : MonoBehaviour
{
    public float distancenow;//距离
    public AudioSource wake;//音效
    public float waitTime;
    public float startTime;
    public float tempTime;
    public float Time3;
    public float chasingTime;
    public float chasingVelocity;
    public float restTime;
    private bool haveTaken = false;
    private bool isChasing = false;
    public  bool beginChasing = false;
    public GameObject myself;
    private Transform playertransform;//player的坐标
    public GameObject chasingTarget;
    void Start()
    {
        playertransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
    }
    void Update()
    {
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        if (isChasing)
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, playertransform.localPosition.y+4, this.transform.localPosition.z);
        }
        if (beginChasing)
        {
            transform.position = UnityEngine.Vector2.MoveTowards(transform.localPosition,chasingTarget.transform.localPosition, chasingVelocity * Time.deltaTime);//追击
        }
        if (playertransform != null && !haveTaken)
        {
            float distance = Mathf.Abs(transform.position.x - playertransform.position.x);//玩家和敌人的距离。
            if (distance <= distancenow)//玩家进入巡逻半径
            {
                haveTaken = true;
                StartCoroutine(Round2());
                wake.Play();
            }
        }
    }


    IEnumerator Round2()
    {
        yield return new WaitForSeconds(startTime);
        for (int q = 40; q > 0; q--)
        {
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
            }
            for (int i = 46; i <= 49; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }//4
            yield return new WaitForSeconds(waitTime);
            yield return new WaitForSeconds(restTime);
            this.transform.localPosition = new Vector3(-129, -346, this.transform.localPosition.z);//下一阶段
            yield return new WaitForSeconds(waitTime);
            float x = transform.localPosition.x - playertransform.localPosition.x;
            for (int i = 0; i < 3; i++)
            {
                isChasing = true;
                yield return new WaitForSeconds(waitTime);
                x = transform.localPosition.x - playertransform.localPosition.x;
                if (x < 0)
                {
                    chasingTarget.transform.localPosition = new Vector3(-45, this.transform.localPosition.y, this.transform.localPosition.z);
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    chasingTarget.transform.localPosition = new Vector3(-290, this.transform.localPosition.y, this.transform.localPosition.z);
                    transform.localScale = new Vector3(1, 1, 1);
                }
                isChasing = false;
                beginChasing = true;
                yield return new WaitForSeconds(chasingTime);
                beginChasing = false;
                this.transform.localPosition = new Vector3(-129, this.transform.localPosition.y, this.transform.localPosition.z);
            }//冲3次
            transform.localScale = new Vector3(1, 1, 1);
            yield return new WaitForSeconds(restTime);
            this.transform.localPosition = new Vector3(-129, -364, this.transform.localPosition.z);//下一阶段
            yield return new WaitForSeconds(Time3);
            this.transform.localPosition = new Vector3(-129, -382, this.transform.localPosition.z);//下一阶段
            yield return new WaitForSeconds(waitTime);

            isChasing = true;
            yield return new WaitForSeconds(waitTime);
            x = transform.localPosition.x - playertransform.localPosition.x;
            if (x < 0)
            {
                chasingTarget.transform.localPosition = new Vector3(-45, this.transform.localPosition.y, this.transform.localPosition.z);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                chasingTarget.transform.localPosition = new Vector3(-290, this.transform.localPosition.y, this.transform.localPosition.z);
                transform.localScale = new Vector3(1, 1, 1);
            }
            isChasing = false;
            beginChasing = true;

            yield return new WaitForSeconds(waitTime);
            for (int i = 6; i < 14; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(1f);
            for (int i = 6; i < 14; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 6; i < 20; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 6; i < 20; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(false);
            }//3
            float y = chasingTime - waitTime * 2 >= 0 ?chasingTime-waitTime*2:0;
            yield return new WaitForSeconds(y);
            beginChasing = false;
            this.transform.localPosition = new Vector3(-129, this.transform.localPosition.y, this.transform.localPosition.z);//dash1

            isChasing = true;
            yield return new WaitForSeconds(waitTime);
            x = transform.localPosition.x - playertransform.localPosition.x;
            if (x < 0)
            {
                chasingTarget.transform.localPosition = new Vector3(-45, this.transform.localPosition.y, this.transform.localPosition.z);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                chasingTarget.transform.localPosition = new Vector3(-290, this.transform.localPosition.y, this.transform.localPosition.z);
                transform.localScale = new Vector3(1, 1, 1);
            }
            isChasing = false;
            beginChasing = true;
            myself.transform.localPosition = new Vector3(-129, -328, this.transform.localPosition.z);
            for (int i = 35; i <= 44; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 35; i <= 44; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(false);
                myself.transform.GetChild(i).localPosition = new Vector3(myself.transform.GetChild(i).localPosition.x - 12, myself.transform.GetChild(i).localPosition.y, myself.transform.GetChild(i).localPosition.z);
            }
            float z = chasingTime - waitTime  >= 0 ? chasingTime - waitTime  : 0;
            yield return new WaitForSeconds(z);
            beginChasing = false;
            this.transform.localPosition = new Vector3(-129, this.transform.localPosition.y, this.transform.localPosition.z);//dash2
            yield return new WaitForSeconds(restTime);//停顿
            isChasing = true;
            yield return new WaitForSeconds(waitTime);
            x = transform.localPosition.x - playertransform.localPosition.x;
            if (x < 0)
            {
                chasingTarget.transform.localPosition = new Vector3(-45, this.transform.localPosition.y, this.transform.localPosition.z);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                chasingTarget.transform.localPosition = new Vector3(-290, this.transform.localPosition.y, this.transform.localPosition.z);
                transform.localScale = new Vector3(1, 1, 1);
            }
            isChasing = false;
            beginChasing = true;

            for (int i = 36; i <= 45; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 35; i <= 45; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(false);
                myself.transform.GetChild(i).localPosition = new Vector3(myself.transform.GetChild(i).localPosition.x + 12, myself.transform.GetChild(i).localPosition.y, myself.transform.GetChild(i).localPosition.z);
            }
            myself.transform.GetChild(45).localPosition = new Vector3(myself.transform.GetChild(45).localPosition.x - 12, myself.transform.GetChild(45).localPosition.y, myself.transform.GetChild(45).localPosition.z);

            myself.transform.localPosition = new Vector3(-174, -306, this.transform.localPosition.z);
            for (int i = 46; i <= 49; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 46; i <= 49; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }//4
            yield return new WaitForSeconds(z);
            beginChasing = false;
            this.transform.localPosition = new Vector3(-129, this.transform.localPosition.y, this.transform.localPosition.z);//dash3

            yield return new WaitForSeconds(restTime);
            this.transform.localPosition = new Vector3(-129, -328, this.transform.localPosition.z);//下一阶段
            yield return new WaitForSeconds(waitTime);
            for (int i = 1; i <= 9; i++)
            {
                transform.GetChild(34).gameObject.SetActive(true);
                yield return new WaitForSeconds(waitTime);
                transform.GetChild(34).gameObject.SetActive(false);
                transform.GetChild(34).localPosition = new Vector3(transform.GetChild(34).localPosition.x, transform.GetChild(34).localPosition.y + 12, transform.GetChild(34).localPosition.z);
            }
            transform.GetChild(34).localPosition = new Vector3(transform.GetChild(34).localPosition.x, transform.GetChild(34).localPosition.y - 12 * 9, transform.GetChild(34).localPosition.z);//1
            yield return new WaitForSeconds(waitTime);
            for (int i = 35; i <= 44; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 35; i <= 44; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                transform.GetChild(i).localPosition = new Vector3(transform.GetChild(i).localPosition.x - 12, transform.GetChild(i).localPosition.y, transform.GetChild(i).localPosition.z);
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
            }
            for (int i = 46; i <= 49; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }//4
            yield return new WaitForSeconds(waitTime);
            yield return new WaitForSeconds(restTime);
            this.transform.localPosition = new Vector3(-129, -346, this.transform.localPosition.z);//下一阶段
            yield return new WaitForSeconds(waitTime);
             x = transform.localPosition.x - playertransform.localPosition.x;
            for (int i = 0; i < 3; i++)
            {
                isChasing = true;
                yield return new WaitForSeconds(waitTime);
                x = transform.localPosition.x - playertransform.localPosition.x;
                if (x < 0)
                {
                    chasingTarget.transform.localPosition = new Vector3(-45, this.transform.localPosition.y, this.transform.localPosition.z);
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    chasingTarget.transform.localPosition = new Vector3(-290, this.transform.localPosition.y, this.transform.localPosition.z);
                    transform.localScale = new Vector3(1, 1, 1);
                }
                isChasing = false;
                beginChasing = true;
                yield return new WaitForSeconds(chasingTime);
                beginChasing = false;
                this.transform.localPosition = new Vector3(-129, this.transform.localPosition.y, this.transform.localPosition.z);
            }//冲3次
            transform.localScale = new Vector3(1, 1, 1);
            yield return new WaitForSeconds(restTime);
            this.transform.localPosition = new Vector3(-129, -364, this.transform.localPosition.z);//下一阶段
            yield return new WaitForSeconds(Time3);
            this.transform.localPosition = new Vector3(-129, -382, this.transform.localPosition.z);//下一阶段
            yield return new WaitForSeconds(waitTime);

            isChasing = true;
            yield return new WaitForSeconds(waitTime);
            x = transform.localPosition.x - playertransform.localPosition.x;
            if (x < 0)
            {
                chasingTarget.transform.localPosition = new Vector3(-45, this.transform.localPosition.y, this.transform.localPosition.z);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                chasingTarget.transform.localPosition = new Vector3(-290, this.transform.localPosition.y, this.transform.localPosition.z);
                transform.localScale = new Vector3(1, 1, 1);
            }
            isChasing = false;
            beginChasing = true;

            yield return new WaitForSeconds(waitTime);
            for (int i = 6; i < 14; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(1f);
            for (int i = 6; i < 14; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 6; i < 20; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 6; i < 20; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(false);
            }//3
             y = chasingTime - waitTime * 2 >= 0 ? chasingTime - waitTime * 2 : 0;
            yield return new WaitForSeconds(y);
            beginChasing = false;
            this.transform.localPosition = new Vector3(-129, this.transform.localPosition.y, this.transform.localPosition.z);//dash1

            isChasing = true;
            yield return new WaitForSeconds(waitTime);
            x = transform.localPosition.x - playertransform.localPosition.x;
            if (x < 0)
            {
                chasingTarget.transform.localPosition = new Vector3(-45, this.transform.localPosition.y, this.transform.localPosition.z);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                chasingTarget.transform.localPosition = new Vector3(-290, this.transform.localPosition.y, this.transform.localPosition.z);
                transform.localScale = new Vector3(1, 1, 1);
            }
            isChasing = false;
            beginChasing = true;
            myself.transform.localPosition = new Vector3(-129, -328, this.transform.localPosition.z);
            for (int i = 35; i <= 44; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 35; i <= 44; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(false);
                myself.transform.GetChild(i).localPosition = new Vector3(myself.transform.GetChild(i).localPosition.x - 12, myself.transform.GetChild(i).localPosition.y, myself.transform.GetChild(i).localPosition.z);
            }
             z = chasingTime - waitTime >= 0 ? chasingTime - waitTime : 0;
            yield return new WaitForSeconds(z);
            beginChasing = false;
            this.transform.localPosition = new Vector3(-129, this.transform.localPosition.y, this.transform.localPosition.z);//dash2
            yield return new WaitForSeconds(restTime);//停顿
            isChasing = true;
            yield return new WaitForSeconds(waitTime);
            x = transform.localPosition.x - playertransform.localPosition.x;
            if (x < 0)
            {
                chasingTarget.transform.localPosition = new Vector3(-45, this.transform.localPosition.y, this.transform.localPosition.z);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                chasingTarget.transform.localPosition = new Vector3(-290, this.transform.localPosition.y, this.transform.localPosition.z);
                transform.localScale = new Vector3(1, 1, 1);
            }
            isChasing = false;
            beginChasing = true;

            for (int i = 36; i <= 45; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(waitTime);
            for (int i = 35; i <= 45; i++)
            {
                myself.transform.GetChild(i).gameObject.SetActive(false);
                myself.transform.GetChild(i).localPosition = new Vector3(myself.transform.GetChild(i).localPosition.x + 12, myself.transform.GetChild(i).localPosition.y, myself.transform.GetChild(i).localPosition.z);
            }
            myself.transform.GetChild(45).localPosition = new Vector3(myself.transform.GetChild(45).localPosition.x - 12, myself.transform.GetChild(45).localPosition.y, myself.transform.GetChild(45).localPosition.z);

            myself.transform.localPosition = new Vector3(-174, -306, this.transform.localPosition.z);
            for (int i = 46; i <= 49; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 46; i <= 49; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }//4
            yield return new WaitForSeconds(z);
            beginChasing = false;
            this.transform.localPosition = new Vector3(-129, this.transform.localPosition.y, this.transform.localPosition.z);//dash3




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
