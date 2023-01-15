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
        this.parameter = manager.parameter;
    }
    public void OnEnter()//����
    {
        parameter.anim.Play("idle");
    }
    public void OnUpdate()//ִ��
    {
        Vector3 pos = manager.transform.position;
        pos.y = parameter.target.position.y;

        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                pos, parameter.moveSpeed * Time.deltaTime);//�����ƶ���Ѳ�ߵ�
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
