using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_ChaseState : EnemyChaseState
{
    private Zombie zombie;

    public Zombie_ChaseState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        zombie = enemy as Zombie;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (distanceToTarget >= stateData.GiveUpRange)
        {
            stateMachine.ChangeState(zombie.idleState);
        }
        else if (distanceToTarget <= zombie.MeleeAttackStateData.attackRange)
        {
            if (Time.time >= zombie.meleeAttackState.lastAttackFinishTime + zombie.MeleeAttackStateData.attackCD) //CD finished
            {
                zombie.meleeAttackState.SetAttackTarget(chaseTarget);
                stateMachine.ChangeState(zombie.meleeAttackState);
            }
        }
    }
}
