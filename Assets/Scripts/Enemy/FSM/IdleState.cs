using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState//空闲状态
{
    private FSM manager;
    private Parameter parameter;
    private int patrolPosition;
    private int cnt = 0;//计数器
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
        float distance = Mathf.Abs(manager.transform.position.y - parameter.target.transform.position.y);
        if (distance <= parameter.warningRadius)
        {

            timer += Time.deltaTime;
            if (cnt < 3)
            {
                Vector3 pos = manager.transform.position;
                pos.y = parameter.target.position.y;

                manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                        pos, parameter.moveSpeed * Time.deltaTime);//敌人移动玩家对应高度
            
            } else
            {
                Vector3 pos = manager.transform.position;
                pos.y = parameter.patrolPoints[patrolPosition].position.y;

                manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                        pos, parameter.moveSpeed * Time.deltaTime);//敌人移动巡逻点对应高度
            }

            if (timer >= parameter.idleTime)
            {
                cnt++;
                manager.TransitionState(StateType.Patrol);//转换成巡逻状态
            }
        }
        if (manager.GetComponent<Enemy>().health < 0)
        {

            manager.TransitionState(StateType.Death);//转换成死亡状态
        }
    }
    public void OnExit()//退出
    {
        timer = 0;
        patrolPosition++;

        if (patrolPosition >= parameter.patrolPoints.Length)//巡逻点下标超出
        {
            cnt = 0;
            /*patrolPosition = 0*/;
        }
    }
}
