using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunState : EnemyState
{
    protected D_StunState stateData;
    protected bool stunFinished;

    public EnemyStunState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        stunFinished = false;
        enemy.navMeshAgent.enabled = false;

        if (stateData.StunBeginEffect != null)
            Object.Instantiate(stateData.StunBeginEffect, enemy.transform.position, Quaternion.identity);
        if (stateData.StunLoopEffect != null)
        {
            var stunLoopEffect = Object.Instantiate(stateData.StunLoopEffect, enemy.transform.position, Quaternion.identity);
            Object.Destroy(stunLoopEffect, stateData.StunTime);
        }
            
    }

    public override void Exit()
    {
        base.Exit();

        enemy.navMeshAgent.enabled = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.StunTime)
        {
            stunFinished = true;
        }
    }
}
