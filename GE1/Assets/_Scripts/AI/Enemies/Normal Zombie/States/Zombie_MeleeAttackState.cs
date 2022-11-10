using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_MeleeAttackState : EnemyMeleeAttackState
{
    private Zombie zombie;

    public Zombie_MeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_MeleeAttackState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        zombie = enemy as Zombie;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAttackFinished)
        {
            stateMachine.ChangeState(zombie.chaseState);
        }
    }
}
