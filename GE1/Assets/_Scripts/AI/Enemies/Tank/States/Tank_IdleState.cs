using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_IdleState : EnemyIdleState
{
    private Tank tank;

    public Tank_IdleState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        tank = enemy as Tank;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(tank.patrolState);
        }
        else if (enemy.PlayerInView)
        {
            stateMachine.ChangeState(tank.chaseState);
        }
    }

}
