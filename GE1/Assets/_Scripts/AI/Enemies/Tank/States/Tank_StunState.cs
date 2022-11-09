using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_StunState : EnemyStunState
{
    private Tank tank;

    public Tank_StunState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        tank = enemy as Tank;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stunFinished)
        {
            stateMachine.ChangeState(tank.chaseState);
        }
    }
}
