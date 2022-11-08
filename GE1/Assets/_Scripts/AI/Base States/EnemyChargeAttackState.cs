using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeAttackState : EnemyAttackState
{
    protected D_ChargeAttackState stateData;

    protected bool isCharging;
    protected bool isCollidingWall;
    protected float chargeStartTime;
    protected Vector3 targetDir;
    public EnemyChargeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_ChargeAttackState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        isCharging = false;
        isCollidingWall = false;
        enemy.anim.SetBool("chargeStarted", false);
        enemy.SetDestination(enemy.transform.position);
        enemy.SetSpeed(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isCharging)
        {
            //Look at player when attacking
            targetDir = (attackTarget.position - enemy.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetDir.x, 0f, targetDir.z));
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5);
        }
        if (Time.time >= startTime + stateData.ChargePrepareTime && !isCharging)
        {
            isCharging = true;
            chargeStartTime = Time.time;

            targetDir = (attackTarget.position - enemy.transform.position).normalized;

            enemy.SetDestination(enemy.transform.position + targetDir * 5f);
            enemy.SetSpeed(stateData.ChargeSpeed);
            enemy.anim.SetBool("chargeStarted", true);
        }
        else if (isCharging)
        {
            enemy.SetDestination(enemy.transform.position + targetDir * 5f);

            if (enemy.DetectingWall)
            {
                isCollidingWall = true;
                AttackFinished();
            }
            else if (enemy.DetectingLedge)
            {
                isAttackFinished = true;
                AttackFinished();
            }
            
            if (Time.time >= chargeStartTime + stateData.MaxChargeDuration)
            {
                AttackFinished();
            }
        }
    }
}
