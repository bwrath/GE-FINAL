using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public Zombie_IdleState idleState { get; private set; }
    public Zombie_PatrolState patrolState { get; private set; }
    public Zombie_ChaseState chaseState { get; private set; }
    public Zombie_MeleeAttackState meleeAttackState { get; private set; }
    public Zombie_DeadState deadState { get; private set; }

    [Header("Scriptable Objects")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PatrolState patrolStateData;
    [SerializeField] private D_ChaseState chaseStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_DeadState deadStateData;

    private BoxCollider hitbox;

    public D_MeleeAttackState MeleeAttackStateData => meleeAttackStateData;

    protected override void Awake()
    {
        base.Awake();

        hitbox = GetComponent<BoxCollider>();
        hitbox.enabled = false;
    }

    protected override void Start()
    {
        base.Start();

        idleState = new Zombie_IdleState(this, StateMachine, "move", idleStateData);
        patrolState = new Zombie_PatrolState(this, StateMachine, "move", patrolStateData);
        chaseState = new Zombie_ChaseState(this, StateMachine, "move", chaseStateData);
        meleeAttackState = new Zombie_MeleeAttackState(this, StateMachine, "meleeAttack", meleeAttackStateData);
        deadState = new Zombie_DeadState(this, StateMachine, "dead", deadStateData);

        StateMachine.Inititalize(patrolState);
    }

    protected override void Update()
    {
        base.Update();

        anim.SetFloat("velocity", navMeshAgent.velocity.magnitude);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (currentHealth <= 0)
            StateMachine.ChangeState(deadState);
    }

    private void EnableHitbox() => hitbox.enabled = true;

    private void DisableHitbox() => hitbox.enabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("damage");
            GetComponent<IDamageable>()?.TakeDamage(enemyBaseData.damage);
        }
    }

    public override void AttackAnimationFinishTrigger()
    {
        meleeAttackState.AttackFinished();
    }
}
