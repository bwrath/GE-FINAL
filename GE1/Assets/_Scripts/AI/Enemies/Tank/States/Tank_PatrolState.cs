using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_PatrolState : EnemyPatrolState
{
    private Tank tank;

    public Tank_PatrolState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_PatrolState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        tank = enemy as Tank;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (reachedCurrentWaypoint)
        {
            stateMachine.ChangeState(tank.idleState);
        }
        else if (enemy.PlayerInView)
        {
            stateMachine.ChangeState(tank.chaseState);
        }
    }
}
