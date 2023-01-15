using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBoss : MonoBehaviour
{
    public float distancenow;//����
    public AudioSource attackMusic;//��Ч
    public float waitTime;
    public float startTime;
    public float temptime;
    private GameObject child1,child0;
    private bool haveTaken = false;
    private Transform playertransform;//player������
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
            float distance = Mathf.Abs(transform.position.x - playertransform.position.x);//��Һ͵��˵ľ��롣Ϊһ��
            if (distance <=distancenow)//��ҽ���Ѳ�߰뾶
            {
                Round1();
                Invoke("Round2", temptime);
              /*  Waitfortime(temptime);
                Round2();*/
                attackMusic.Play();
                haveTaken = true;
            }
        }
    }
    void Round1()
    {
        child0.SetActive(true);
        child1.SetActive(true);
    }
    void Round2()
    {
        for(int i=2;i<9 ;Waitfortime(waitTime),i++ )
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    IEnumerator Waitfortime(float time)
    {
        yield return new WaitForSeconds(time);//��ʱ
    }
    public void StopAttack()
    {
      //  attack.SetBool("attack", false);
    }
}
