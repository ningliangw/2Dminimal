using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState//����״̬
{
    private FSM manager;
    private Parameter parameter;
    private int patrolPosition;
    private int cnt = 0;//������
    private float timer;
    
    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()//����
    {
        parameter.anim.Play("idle");

    }
    public void OnUpdate()//ִ��
    {
        float distance = Mathf.Abs(manager.transform.position.y - parameter.target.transform.position.y);
        if (distance <= parameter.warningRadius)
        {

            timer += Time.deltaTime;
            if (cnt < 3)
            {
                Vector3 pos = manager.transform.position;
                pos.y = parameter.target.position.y;

                manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                        pos, parameter.moveSpeed * Time.deltaTime);//�����ƶ���Ҷ�Ӧ�߶�
            
            } else
            {
                Vector3 pos = manager.transform.position;
                pos.y = parameter.patrolPoints[patrolPosition].position.y;

                manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                        pos, parameter.moveSpeed * Time.deltaTime);//�����ƶ�Ѳ�ߵ��Ӧ�߶�
            }

            if (timer >= parameter.idleTime)
            {
                cnt++;
                manager.TransitionState(StateType.Patrol);//ת����Ѳ��״̬
            }
        }
        if (manager.GetComponent<Enemy>().health < 0)
        {

            manager.TransitionState(StateType.Death);//ת��������״̬
        }
    }
    public void OnExit()//�˳�
    {
        timer = 0;
        patrolPosition++;

        if (patrolPosition >= parameter.patrolPoints.Length)//Ѳ�ߵ��±곬��
        {
            cnt = 0;
            /*patrolPosition = 0*/;
        }
    }
}
