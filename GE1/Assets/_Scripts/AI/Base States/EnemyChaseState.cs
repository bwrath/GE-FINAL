using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    protected D_ChaseState stateData;
    protected Transform chaseTarget;

    protected float distanceToTarget;

    public EnemyChaseState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        chaseTarget ??= enemy.fov.visibleTargets[0]; //The fov system sometimes fails to find the target if they are too near, so a check is done
        enemy.SetDestination(chaseTarget.position);
        enemy.SetSpeed(stateData.ChaseSpeed);
    }

    public override void Exit()
    {
        base.Exit();

        if (distanceToTarget >= stateData.GiveUpRange)
            chaseTarget = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.SetDestination(chaseTarget.position);
        distanceToTarget = Vector3.Distance(enemy.transform.position, chaseTarget.position);
    }

    public virtual void SetChaseTarget(Transform target)
    {
        chaseTarget = target;
    }
}
