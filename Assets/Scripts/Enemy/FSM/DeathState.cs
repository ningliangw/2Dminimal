using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState//死亡状态
{
    private FSM manager;
    private Parameter parameter;

    public DeathState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()//进入
    {
        parameter.anim.Play("died");

    }
    public void OnUpdate()//执行
    {
        
    }
    public void OnExit()//退出
    {
        
    }
}
