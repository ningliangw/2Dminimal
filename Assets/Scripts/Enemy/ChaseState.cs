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
        parameter.anim.Play("attack");
    }
    public void OnUpdate()//执行
    {
        AnimatorStateInfo info = parameter.anim.GetCurrentAnimatorStateInfo(0);
        /*if (manager.tag == "eyeboss")
        {
            Vector3 pos = manager.transform.position;
            pos.y = parameter.target.position.y;
            manager.transform.position = pos;
        }*/
        if (info.normalizedTime >= 1f)
        {
            manager.Flip(parameter.patrolPoints[patrolPosition]);//敌人朝向

            Vector3 pos = parameter.patrolPoints[patrolPosition].position;
            pos.y = manager.transform.position.y;

            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                pos, parameter.chaseSpeed * Time.deltaTime);//敌人移动到巡逻点
            if (Vector2.Distance(manager.transform.position, pos) < 6f)
            {
                manager.TransitionState(StateType.Idle);//转换成空闲状态
            }
        }

    }
    public void OnExit()//退出
    {
        patrolPosition++;

        if (patrolPosition >= parameter.patrolPoints.Length)//巡逻点下标超出
        {
            patrolPosition = 0;
        }
    }
}
