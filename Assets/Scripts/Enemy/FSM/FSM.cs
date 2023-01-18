using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType//枚举状态类型
{
    Idle, Patrol, Chase, Death//空闲，巡逻，追踪，死亡
}

[Serializable]//序列化
public class Parameter
{
    public float moveSpeed;
    public float chaseSpeed;        //追踪速度
    public float idleTime;
    public float radius;//距离
    public float warningRadius;//预警距离（boss）
    public Transform[] patrolPoints;//巡逻点
    public Transform target;
    public Animator anim;
    public AudioSource moveAudio;
    public AudioSource attackAudio;
}


public class FSM : MonoBehaviour//有限状态机
{
    public Parameter parameter;//参数

    private IState currentState;//当前状态
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();//字典存放状态
    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));//添加状态
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.Death, new DeathState(this));

        transform.DetachChildren();

        TransitionState(StateType.Idle);//初始状态为idle

        parameter.anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(StateType type)//转换状态
    {
        if (currentState != null)
        {
            currentState.OnExit();//退出当前状态
        }
        currentState = states[type];
        currentState.OnEnter();
    }

    public void Flip(Transform target)//改变敌人朝向
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
