using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public Zombie_IdleState idleState { get; private set; }
    public Zombie_PatrolState patrolState { get; private set; }
    public Zombie_ChaseState chaseState { get; private set; }
    public Zombie_AttackState attackState { get; private set; }

    [Header("Scriptable Objects")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PatrolState patrolStateData;
    [SerializeField] private D_ChaseState chaseStateData;
    [SerializeField] private D_AttackState attackStateData;

    public D_AttackState AttackStateData => attackStateData;

    protected override void Start()
    {
        base.Start();

        idleState = new Zombie_IdleState(this, StateMachine, "move", idleStateData);
        patrolState = new Zombie_PatrolState(this, StateMachine, "move", patrolStateData);
        chaseState = new Zombie_ChaseState(this, StateMachine, "move", chaseStateData);
        attackState = new Zombie_AttackState(this, StateMachine, "attack", attackStateData);

        StateMachine.Inititalize(patrolState);
    }

    protected override void Update()
    {
        base.Update();

        anim.SetFloat("velocity", navMeshAgent.velocity.magnitude);
    }

    private void AttackAnimationFinishTrigger()
    {
        attackState.AttackAnimationFinished();
    }
}
