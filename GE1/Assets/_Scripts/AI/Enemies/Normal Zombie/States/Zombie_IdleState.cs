using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_IdleState : EnemyIdleState
{
    private Zombie zombie;

    public Zombie_IdleState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        zombie = enemy as Zombie;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(zombie.patrolState);
        }
    }
}
