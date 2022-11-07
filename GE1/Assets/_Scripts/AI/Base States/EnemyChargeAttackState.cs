using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeAttackState : EnemyAttackState
{
    protected D_ChargeAttackState stateData;

    protected bool isCharging;
    protected float chargeStartTime;

    public EnemyChargeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_ChargeAttackState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        isCharging = false;
        enemy.anim.SetBool("chargeStarted", false);
        enemy.SetSpeed(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.ChargePrepareTime && !isCharging)
        {
            isCharging = true;
            chargeStartTime = Time.time;

            Vector3 targetDir = (attackTarget.position - enemy.transform.position).normalized;

            enemy.SetDestination(targetDir * 20);
            enemy.SetSpeed(stateData.ChargeSpeed);
            enemy.anim.SetBool("chargeStarted", true);
        }
        else if (isCharging)
        {
            if (Time.time >= chargeStartTime + stateData.MaxChargeDuration)
            {
                isAttackFinished = true;
            }
        }
    }
}
