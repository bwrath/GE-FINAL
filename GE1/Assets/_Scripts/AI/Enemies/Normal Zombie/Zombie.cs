using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public Zombie_IdleState idleState { get; private set; }
    public Zombie_PatrolState patrolState { get; private set; }
    public Zombie_ChaseState chaseState { get; private set; }
    public Zombie_MeleeAttackState meleeAttackState { get; private set; }

    [Header("Scriptable Objects")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PatrolState patrolStateData;
    [SerializeField] private D_ChaseState chaseStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;

    public D_MeleeAttackState MeleeAttackStateData => meleeAttackStateData;

    protected override void Start()
    {
        base.Start();

        idleState = new Zombie_IdleState(this, StateMachine, "move", idleStateData);
        patrolState = new Zombie_PatrolState(this, StateMachine, "move", patrolStateData);
        chaseState = new Zombie_ChaseState(this, StateMachine, "move", chaseStateData);
        meleeAttackState = new Zombie_MeleeAttackState(this, StateMachine, "meleeAttack", meleeAttackStateData);

        StateMachine.Inititalize(patrolState);
    }

    protected override void Update()
    {
        base.Update();

        anim.SetFloat("velocity", navMeshAgent.velocity.magnitude);
    }

    public override void AttackAnimationFinishTrigger()
    {
        meleeAttackState.AttackFinished();
    }
}
