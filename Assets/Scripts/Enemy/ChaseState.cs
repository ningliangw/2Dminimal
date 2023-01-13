using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState//追踪状态
{
    private FSM manager;
    private Parameter parameter;
    private int patrolPosition;
    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()//进入
    {
        parameter.anim.Play("move");
    }
    public void OnUpdate()//执行
    {
        manager.Flip(parameter.target);//敌人朝向

        if (parameter.target)
        {
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            parameter.target.position, parameter.chaseSpeed * Time.deltaTime);//追逐玩家
        }
        if (parameter.target == null)
        {

        }

    }
    public void OnExit()//退出
    {
        
    }
}
