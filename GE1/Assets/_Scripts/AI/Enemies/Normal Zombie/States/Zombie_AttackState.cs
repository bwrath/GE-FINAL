using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_AttackState : EnemyAttackState
{
    private Zombie zombie;

    public Zombie_AttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_AttackState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
        zombie = enemy as Zombie;
    }

    public override void Enter()
    {
        base.Enter();

        isAttackFinished = false;
        enemy.SetSpeed(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Look at player when attacking
        Vector3 targetDir = (attackTarget.position - enemy.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetDir.x, 0f, targetDir.z));
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5);

        if (isAttackFinished)
        {
            lastAttackFinishTime = Time.time;
            stateMachine.ChangeState(zombie.chaseState);
        }
    }

    public override void AttackAnimationFinished()
    {
        base.AttackAnimationFinished();

        isAttackFinished = true;
    }
}
