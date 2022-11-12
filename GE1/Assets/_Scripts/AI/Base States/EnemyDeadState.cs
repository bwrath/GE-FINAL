using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyState
{
    protected D_DeadState stateData;

    public EnemyDeadState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.navMeshAgent.enabled = false;
        enemy.ToggleRagdoll(true);
        Object.Destroy(enemy.gameObject, stateData.destroyTime);
    }

}
