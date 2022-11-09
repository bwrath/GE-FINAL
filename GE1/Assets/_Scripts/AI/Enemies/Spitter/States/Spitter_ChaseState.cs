using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter_ChaseState : EnemyChaseState
{
    private Spitter spitter;

    public Spitter_ChaseState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        spitter = enemy as Spitter;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (distanceToTarget >= stateData.GiveUpRange)
        {
            stateMachine.ChangeState(spitter.idleState);
        }
        else if (Time.time >= spitter.rangedAttackState.lastAttackFinishTime + spitter.RangedAttackStateData.RangedAttackCD) //CD finished and no obstruction
        {
            if (distanceToTarget <= spitter.RangedAttackStateData.AttackRange)
            {
                spitter.rangedAttackState.SetAttackTarget(chaseTarget);
                stateMachine.ChangeState(spitter.rangedAttackState);
            }
        }
    }
}
