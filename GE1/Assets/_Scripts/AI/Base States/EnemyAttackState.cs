using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected D_AttackState stateData;
    protected Transform attackTarget;

    protected bool isAttackFinished;

    public float lastAttackFinishTime { get; protected set; }

    public EnemyAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_AttackState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Exit()
    {
        base.Exit();

        attackTarget = null;
    }

    public virtual void AttackAnimationFinished()
    {

    }

    public virtual void SetAttackTarget(Transform target) => attackTarget = target;
}
