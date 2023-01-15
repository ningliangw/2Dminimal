using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState//巡逻状态
{
    private FSM manager;
    private Parameter parameter;
    private int patrolPosition;
    public PatrolState(FSM manager)
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
        manager.Flip(parameter.patrolPoints[patrolPosition]);//敌人朝向

        manager.transform.position = Vector2.MoveTowards(manager.transform.position, 
            parameter.patrolPoints[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);//敌人移动到巡逻点
        if (Vector2.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < 6f)
        {
            manager.TransitionState(StateType.Idle);//转换成空闲状态
        }

        float distance = manager.transform.position.x - parameter.target.transform.position.x;
        if (distance < 0 && Mathf.Abs(distance) <= parameter.radius && manager.transform.localScale.x < 0)//怪物在左
        {
            manager.TransitionState(StateType.Chase);//转换成攻击状态
            SoundMananger.instance.EnemyAttack();
        }
        if (distance > 0 && distance <= parameter.radius && manager.transform.localScale.x > 0)//怪物在右
        {
            manager.TransitionState(StateType.Chase);//转换成攻击状态
            SoundMananger.instance.EnemyAttack();
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
