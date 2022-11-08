using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackState
{
    protected D_MeleeAttackState stateData;

    public EnemyMeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_MeleeAttackState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetSpeed(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Look at player when attacking
        Vector3 targetDir = (attackTarget.position - enemy.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetDir.x, 0f, targetDir.z));
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5);
    }
}
