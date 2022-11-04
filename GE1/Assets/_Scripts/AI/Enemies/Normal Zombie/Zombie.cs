using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public Zombie_IdleState idleState { get; private set; }
    public Zombie_PatrolState patrolState { get; private set; }

    [Header("Scriptable Objects")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PatrolState patrolStateData;

    protected override void Start()
    {
        base.Start();

        idleState = new Zombie_IdleState(this, StateMachine, "idle", idleStateData);
        patrolState = new Zombie_PatrolState(this, StateMachine, "patrol", patrolStateData);

        StateMachine.Inititalize(patrolState);
    }
}
