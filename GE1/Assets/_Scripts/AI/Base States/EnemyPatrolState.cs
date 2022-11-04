using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    protected D_PatrolState stateData;

    private Vector3[] waypoints;

    protected bool reachedCurrentWaypoint;

    public EnemyPatrolState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_PatrolState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        reachedCurrentWaypoint = false;

        enemy.navMeshAgent.speed = stateData.PatrolSpeed;
        enemy.navMeshAgent.stoppingDistance = stateData.StoppingDistance;

        waypoints = enemy.Waypoints;
        enemy.SetDestination(enemy.originalPosition + waypoints[enemy.currentWaypoint]);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Vector3.Distance(enemy.transform.position, enemy.originalPosition + waypoints[enemy.currentWaypoint]) <= enemy.navMeshAgent.stoppingDistance)
        {
            enemy.WaypointReached();
            reachedCurrentWaypoint = true;
        }
    }

}
