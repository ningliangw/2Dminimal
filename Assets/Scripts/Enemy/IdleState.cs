using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState//空闲状态
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()//进入
    {
        parameter.anim.Play("idle");
    }
    public void OnUpdate()//执行
    {
        timer += Time.deltaTime;
        Vector3 pos = manager.transform.position;
        pos.y = parameter.target.position.y;

        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                pos, parameter.moveSpeed * Time.deltaTime);//敌人移动到巡逻点

        if (timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);//转换成巡逻状态
        }
    }
    public void OnExit()//退出
    {
        timer = 0;
    }
}
