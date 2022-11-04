using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    protected D_ChaseState stateData;
    protected Transform chaseTarget;

    public EnemyChaseState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetDestination(chaseTarget.position);
        enemy.SetSpeed(stateData.ChaseSpeed);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.SetDestination(chaseTarget.position);
    }
}
