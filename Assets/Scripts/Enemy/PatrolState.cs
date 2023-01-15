using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState//Ѳ��״̬
{
    private FSM manager;
    private Parameter parameter;
    private int patrolPosition;
    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()//����
    {
        parameter.anim.Play("move");
    }
    public void OnUpdate()//ִ��
    {
        manager.Flip(parameter.patrolPoints[patrolPosition]);//���˳���

        manager.transform.position = Vector2.MoveTowards(manager.transform.position, 
            parameter.patrolPoints[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);//�����ƶ���Ѳ�ߵ�
        if (Vector2.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < 6f)
        {
            manager.TransitionState(StateType.Idle);//ת���ɿ���״̬
        }

        float distance = manager.transform.position.x - parameter.target.transform.position.x;
        if (distance < 0 && Mathf.Abs(distance) <= parameter.radius && manager.transform.localScale.x < 0)//��������
        {
            manager.TransitionState(StateType.Chase);//ת���ɹ���״̬
            SoundMananger.instance.EnemyAttack();
        }
        if (distance > 0 && distance <= parameter.radius && manager.transform.localScale.x > 0)//��������
        {
            manager.TransitionState(StateType.Chase);//ת���ɹ���״̬
            SoundMananger.instance.EnemyAttack();
        }

    }
    public void OnExit()//�˳�
    {
        patrolPosition++;

        if (patrolPosition >= parameter.patrolPoints.Length)//Ѳ�ߵ��±곬��
        {
            patrolPosition = 0;
        }
    }
}
