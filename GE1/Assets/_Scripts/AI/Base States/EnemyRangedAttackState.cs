using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackState : EnemyAttackState
{
    protected D_RangedAttackState stateData;
    protected Rigidbody projectileRB;

    protected Vector3 projectileInitialPos = Vector3.zero;
    protected bool projectileFired;

    public EnemyRangedAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_RangedAttackState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        projectileFired = false;
        enemy.SetDestination(enemy.transform.position);
        enemy.SetSpeed(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Vector3 targetDir = (attackTarget.position - enemy.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetDir.x, 0f, targetDir.z));
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5);

        if (Time.time >= startTime + stateData.RangedAttackPrepareTime && !projectileFired)
        {
            projectileFired = true;
            GameObject projectile = GameObject.Instantiate(stateData.projectile, projectileInitialPos, Quaternion.identity);

            if (projectile.TryGetComponent(out projectileRB))
            {
                projectileRB.useGravity = stateData.AffectedByGravity;
                projectileRB.AddForce(targetDir * stateData.ProjectileForwardForce + enemy.transform.up * stateData.ProjectileUpwardForce, ForceMode.Impulse);
            }

            Debug.Log(projectile.transform.position);
            projectile.GetComponent<Projectile>()?.SetDamage(enemy.EnemyBaseData.damage);

            AttackFinished();
        }
    }
}
