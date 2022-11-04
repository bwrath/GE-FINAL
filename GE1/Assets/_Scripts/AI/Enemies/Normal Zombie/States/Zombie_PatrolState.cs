using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_PatrolState : EnemyPatrolState
{
    private Zombie zombie;

    public Zombie_PatrolState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_PatrolState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        zombie = enemy as Zombie;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (reachedCurrentWaypoint)
        {
            stateMachine.ChangeState(zombie.idleState);
        }
    }
}
