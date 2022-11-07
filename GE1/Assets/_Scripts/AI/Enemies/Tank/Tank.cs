using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    public Tank_IdleState idleState { get; private set; }
    public Tank_PatrolState patrolState { get; private set; }
    public Tank_ChaseState chaseState { get; private set; }
    public Tank_ChargeAttackState chargeAttackState { get; private set; }

    [Header("Scriptable Objects")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PatrolState patrolStateData;
    [SerializeField] private D_ChaseState chaseStateData;
    [SerializeField] private D_ChargeAttackState chargeAttackStateData;

    public D_ChargeAttackState ChargeAttackStateData => chargeAttackStateData;

    protected override void Start()
    {
        base.Start();

        idleState = new Tank_IdleState(this, StateMachine, "move", idleStateData);
        patrolState = new Tank_PatrolState(this, StateMachine, "move", patrolStateData);
        chaseState = new Tank_ChaseState(this, StateMachine, "move", chaseStateData);
        chargeAttackState = new Tank_ChargeAttackState(this, StateMachine, "chargeAttack", chargeAttackStateData);

        StateMachine.Inititalize(patrolState);
    }

    protected override void Update()
    {
        base.Update();

        anim.SetFloat("velocity", navMeshAgent.velocity.magnitude);
    }
}
