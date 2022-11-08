using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter_RangedAttackState : EnemyRangedAttackState
{
    private Spitter spitter;

    public Spitter_RangedAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_RangedAttackState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        spitter = enemy as Spitter;
    }

    public override void LogicUpdate()
    {
        projectileInitialPos = spitter.RangedAttackPosition.position;

        base.LogicUpdate();

        if (isAttackFinished)
        {
            stateMachine.ChangeState(spitter.idleState);
        }
    }
}
