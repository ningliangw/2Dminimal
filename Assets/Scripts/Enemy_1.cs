using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private bool faceleft = true;
    private float leftx, rightx;
    private float stopTime = 0f;//停留时间
    public float patrolSpeed;//巡逻时间
    public float patrolWaitTime = 1f;  //巡逻等待时间
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
                rb.velocity = Vector2.zero;//速度设为0
                stopTime += Time.deltaTime;//停留时间自增
                if (stopTime >= patrolWaitTime)//如果停留了等待时间
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    faceleft = false;//向右
                    stopTime = 0;//停留时间置为0
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(patrolSpeed, rb.velocity.y);
            if (transform.position.x > rightx)
            {
                rb.velocity = Vector2.zero;//速度设为0
                stopTime += Time.deltaTime;//停留时间自增
                if (stopTime >= patrolWaitTime)//如果停留了等待时间
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    faceleft = true;//向左
                    stopTime = 0;//停留时间置为0
                }
            }
        }
    }
}
