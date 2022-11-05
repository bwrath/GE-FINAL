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
        else if (distanceToTarget <= stateData.ActionRange)
        {
            if (Time.time >= zombie.attackState.lastAttackFinishTime + zombie.AttackStateData.attackCD) //CD finished
            {
                zombie.attackState.SetAttackTarget(chaseTarget);
                stateMachine.ChangeState(zombie.attackState);
            }
        }
    }
}
