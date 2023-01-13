using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType//ö��״̬����
{
    Idle, Patrol, Chase, Attack//���У�Ѳ�ߣ�׷�٣�����
}

[Serializable]//���л�
public class Parameter
{
    public float moveSpeed;
    public float chaseSpeed;        //׷���ٶ�
    public float idleTime;
    public Transform[] patrolPoints;//Ѳ�ߵ�
    public Transform[] chasePoints; //׷�ٵ�
    public Transform target;
    public Animator anim;
}


public class FSM : MonoBehaviour//����״̬��
{
    public Parameter parameter;//����

    private IState currentState;//��ǰ״̬
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();//�ֵ���״̬
    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));//���״̬
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));

        transform.DetachChildren();

        TransitionState(StateType.Idle);//��ʼ״̬Ϊidle

        parameter.anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(StateType type)//ת��״̬
    {
        if (currentState != null)
        {
            currentState.OnExit();//�˳���ǰ״̬
        }
        currentState = states[type];
        currentState.OnEnter();
    }

    public void Flip(Transform target)//�ı���˳���
    {
        if (target != null)
        {
            if (transform.position.x > target .position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (transform.position.x < target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
