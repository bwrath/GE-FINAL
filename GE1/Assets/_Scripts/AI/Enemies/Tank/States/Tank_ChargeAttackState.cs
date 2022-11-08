using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_ChargeAttackState : EnemyChargeAttackState
{
    private Tank tank;

    public Tank_ChargeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_ChargeAttackState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        tank = enemy as Tank;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAttackFinished)
        {
            if (isCollidingWall)
            {
                enemy.SetDestination(enemy.transform.position);
                enemy.SetSpeed(0f);
                stateMachine.ChangeState(tank.stunState);
            }
            else
            {
                stateMachine.ChangeState(tank.chaseState);
            }
        }
    }
}
