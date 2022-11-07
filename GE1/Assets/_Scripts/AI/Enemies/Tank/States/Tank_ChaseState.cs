using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_ChaseState : EnemyChaseState
{
    private Tank tank;

    public Tank_ChaseState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        tank = enemy as Tank;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (distanceToTarget >= stateData.GiveUpRange)
        {
            stateMachine.ChangeState(tank.idleState);
        }
        else if (distanceToTarget <= stateData.ActionRange)
        {
            if (Time.time >= tank.chargeAttackState.lastAttackFinishTime + tank.ChargeAttackStateData.ChargeAttackCD) //CD finished
            {
                tank.chargeAttackState.SetAttackTarget(chaseTarget);
                stateMachine.ChangeState(tank.chargeAttackState);
            }
        }
    }
}
