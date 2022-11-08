using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy
{
    public Spitter_IdleState idleState { get; private set; }
    public Spitter_PatrolState patrolState { get; private set; }
    public Spitter_ChaseState chaseState { get; private set; }
    public Spitter_RangedAttackState rangedAttackState { get; private set; }

    [Header("Scriptable Objects")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PatrolState patrolStateData;
    [SerializeField] private D_ChaseState chaseStateData;
    [SerializeField] private D_RangedAttackState rangedAttackStateData;

    [Header("Ranged Attack")]
    [SerializeField] private Transform rangedAttackPosition;

    public Transform RangedAttackPosition => rangedAttackPosition;
    public D_RangedAttackState RangedAttackStateData => rangedAttackStateData;

    protected override void Start()
    {
        base.Start();

        idleState = new Spitter_IdleState(this, StateMachine, "move", idleStateData);
        patrolState = new Spitter_PatrolState(this, StateMachine, "move", patrolStateData);
        chaseState = new Spitter_ChaseState(this, StateMachine, "move", chaseStateData);
        rangedAttackState = new Spitter_RangedAttackState(this, StateMachine, "rangedAttack", rangedAttackStateData);

        StateMachine.Inititalize(patrolState);
    }

    protected override void Update()
    {
        base.Update();

        anim.SetFloat("velocity", navMeshAgent.velocity.magnitude);
    }

    public override void AttackAnimationFinishTrigger()
    {
        rangedAttackState.AttackFinished();
    }
}
