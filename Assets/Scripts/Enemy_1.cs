using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private bool faceleft = true;
    private float leftx, rightx;
    private float stopTime = 0f;//ͣ��ʱ��
    public float patrolSpeed;//Ѳ��ʱ��
    public float patrolWaitTime = 1f;  //Ѳ�ߵȴ�ʱ��
    public Transform leftPos, rightPos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        leftx = leftPos.position.x;
        rightx = rightPos.position.x;
        Destroy(leftPos.gameObject);
        Destroy(rightPos.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (faceleft)
        {
            rb.velocity = new Vector2(-patrolSpeed, rb.velocity.y);
            if (transform.position.x < leftx)
            {
                rb.velocity = Vector2.zero;//�ٶ���Ϊ0
                stopTime += Time.deltaTime;//ͣ��ʱ������
                if (stopTime >= patrolWaitTime)//���ͣ���˵ȴ�ʱ��
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    faceleft = false;//����
                    stopTime = 0;//ͣ��ʱ����Ϊ0
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(patrolSpeed, rb.velocity.y);
            if (transform.position.x > rightx)
            {
                rb.velocity = Vector2.zero;//�ٶ���Ϊ0
                stopTime += Time.deltaTime;//ͣ��ʱ������
                if (stopTime >= patrolWaitTime)//���ͣ���˵ȴ�ʱ��
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    faceleft = true;//����
                    stopTime = 0;//ͣ��ʱ����Ϊ0
                }
            }
        }
    }
}
