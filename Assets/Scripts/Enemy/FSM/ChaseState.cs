using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState//׷??״̬
{
    private FSM manager;
    private Parameter parameter;
    private int patrolPosition;
    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()//????
    {
        parameter.anim.Play("attack");
        parameter.attackAudio.Play();
    }
    public void OnUpdate()//ִ??
    {
        AnimatorStateInfo info = parameter.anim.GetCurrentAnimatorStateInfo(0);
        
        if (info.normalizedTime >= 1f)
        {
            manager.Flip(parameter.patrolPoints[patrolPosition]);//???˳???

            Vector3 pos = parameter.patrolPoints[patrolPosition].position;
            pos.y = manager.transform.position.y;

            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                pos, parameter.chaseSpeed * Time.deltaTime);//?????ƶ???Ѳ?ߵ?
            if (Vector2.Distance(manager.transform.position, pos) < 6f)
            {
                manager.TransitionState(StateType.Idle);//ת???ɿ???״̬
            }
        }
        if (manager.GetComponent<Enemy>().health < 0)
        {
            manager.TransitionState(StateType.Death);//ת????????״̬
        }

    }
    public void OnExit()//?˳?
    {
        patrolPosition++;

        if (patrolPosition >= parameter.patrolPoints.Length)//Ѳ?ߵ??±곬??
        {
            patrolPosition = 0;
        }
    }
}
