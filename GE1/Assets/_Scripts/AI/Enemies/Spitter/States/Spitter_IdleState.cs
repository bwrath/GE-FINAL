using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter_IdleState : EnemyIdleState
{
    private Spitter spitter;

    public Spitter_IdleState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        spitter = enemy as Spitter;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(spitter.patrolState);
        }
        else if (enemy.PlayerInView)
        {
            stateMachine.ChangeState(spitter.chaseState);
        }
    }
}
