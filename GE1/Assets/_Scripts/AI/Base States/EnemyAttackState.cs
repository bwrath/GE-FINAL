using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected Transform attackTarget;

    protected bool isAttackFinished;

    public float lastAttackFinishTime { get; protected set; } = Mathf.NegativeInfinity;

    public EnemyAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        isAttackFinished = false;
    }

    public override void Exit()
    {
        base.Exit();

        attackTarget = null;
    }

    public virtual void AttackFinished()
    {
        isAttackFinished = true;
        lastAttackFinishTime = Time.time;
    }

    public virtual void SetAttackTarget(Transform target) => attackTarget = target;
}
