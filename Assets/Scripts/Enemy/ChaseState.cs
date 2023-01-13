using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState//׷��״̬
{
    private FSM manager;
    private Parameter parameter;
    private int patrolPosition;
    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()//����
    {
        parameter.anim.Play("move");
    }
    public void OnUpdate()//ִ��
    {
        manager.Flip(parameter.target);//���˳���

        if (parameter.target)
        {
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            parameter.target.position, parameter.chaseSpeed * Time.deltaTime);//׷�����
        }
        if (parameter.target == null)
        {

        }

    }
    public void OnExit()//�˳�
    {
        
    }
}
