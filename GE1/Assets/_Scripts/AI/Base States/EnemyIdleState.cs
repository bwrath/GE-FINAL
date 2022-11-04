using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected D_IdleState stateData;

    protected bool isIdleTimeOver;

    public EnemyIdleState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        isIdleTimeOver = false;
        enemy.SetDestination(enemy.transform.position);
        enemy.SetSpeed(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.IdleTime)
            isIdleTimeOver = true;
    }
}
