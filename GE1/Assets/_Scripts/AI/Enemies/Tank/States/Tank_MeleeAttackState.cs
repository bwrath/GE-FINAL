using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_MeleeAttackState :  EnemyMeleeAttackState
{
    private Tank tank;

    public Tank_MeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_MeleeAttackState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        tank = enemy as Tank;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAttackFinished)
        {
            stateMachine.ChangeState(tank.chaseState);
        }
    }
}
