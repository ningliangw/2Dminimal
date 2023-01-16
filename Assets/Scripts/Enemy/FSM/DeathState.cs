using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState//����״̬
{
    private FSM manager;
    private Parameter parameter;

    public DeathState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()//����
    {
        parameter.anim.Play("died");

    }
    public void OnUpdate()//ִ��
    {
        
    }
    public void OnExit()//�˳�
    {
        
    }
}
