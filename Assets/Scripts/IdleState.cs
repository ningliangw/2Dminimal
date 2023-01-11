using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState//����״̬
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = parameter;
    }
    public void OnEnter()//����
    {
        parameter.anim.Play("Idle");
    }
    public void OnUpdate()//ִ��
    {
        timer += Time.deltaTime;

        if (timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);//ת����Ѳ��״̬
        }
    }
    public void OnExit()//�˳�
    {
        timer = 0;
    }
}
