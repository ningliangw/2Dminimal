using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState//Ѳ��״̬
{
    private FSM manager;
    private Parameter parameter;
    private int patrolPosition;
    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = parameter;
    }
    public void OnEnter()//����
    {
        parameter.anim.Play("Walk");
    }
    public void OnUpdate()//ִ��
    {
        manager.Flip(parameter.patrolPoints[patrolPosition]);//���˳���

        manager.transform.position = Vector2.MoveTowards(manager.transform.position, 
            parameter.patrolPoints[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);//�����ƶ���Ѳ�ߵ�
        if (Vector2.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < 0.1f)
        {
            manager.TransitionState(StateType.Idle);//ת���ɿ���״̬
        }

    }
    public void OnExit()//�˳�
    {
        patrolPosition++;

        if (patrolPosition >= parameter.patrolPoints.Length)//Ѳ�ߵ��±곬��
        {
            patrolPosition = 0;
        }
    }
}
