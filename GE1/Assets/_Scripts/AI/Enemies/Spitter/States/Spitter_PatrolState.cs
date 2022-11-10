using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter_PatrolState : EnemyPatrolState
{
    private Spitter spitter;

    public Spitter_PatrolState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_PatrolState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        spitter = enemy as Spitter;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (reachedCurrentWaypoint)
        {
            stateMachine.ChangeState(spitter.idleState);
        }
        else if (enemy.PlayerInView)
        {
            stateMachine.ChangeState(spitter.chaseState);
        }
    }
}
