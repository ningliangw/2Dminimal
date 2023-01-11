using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState//¿ÕÏÐ×´Ì¬
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = parameter;
    }
    public void OnEnter()//½øÈë
    {
        parameter.anim.Play("Idle");
    }
    public void OnUpdate()//Ö´ÐÐ
    {
        timer += Time.deltaTime;

        if (timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);//×ª»»³ÉÑ²Âß×´Ì¬
        }
    }
    public void OnExit()//ÍË³ö
    {
        timer = 0;
    }
}
