using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    public Tank_IdleState idleState { get; private set; }
    public Tank_PatrolState patrolState { get; private set; }
    public Tank_ChaseState chaseState { get; private set; }
    public Tank_MeleeAttackState meleeAttackState { get; private set; }
    public Tank_ChargeAttackState chargeAttackState { get; private set; }
    public Tank_StunState stunState { get; private set; }
    public Tank_DeadState deadState { get; private set; }

    [Header("Scriptable Objects")]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PatrolState patrolStateData;
    [SerializeField] private D_ChaseState chaseStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_ChargeAttackState chargeAttackStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;

    public D_MeleeAttackState MeleeAttackStateData => meleeAttackStateData;
    public D_ChargeAttackState ChargeAttackStateData => chargeAttackStateData;

    private BoxCollider hitbox;
    private bool damaged = false;

    protected override void Awake()
    {
        base.Awake();

        hitbox = GetComponent<BoxCollider>();
        hitbox.enabled = false;
    }

    protected override void Start()
    {
        base.Start();

        idleState = new Tank_IdleState(this, StateMachine, "move", idleStateData);
        patrolState = new Tank_PatrolState(this, StateMachine, "move", patrolStateData);
        chaseState = new Tank_ChaseState(this, StateMachine, "move", chaseStateData);
        meleeAttackState = new Tank_MeleeAttackState(this, StateMachine, "meleeAttack", meleeAttackStateData);
        chargeAttackState = new Tank_ChargeAttackState(this, StateMachine, "chargeAttack", chargeAttackStateData);
        stunState = new Tank_StunState(this, StateMachine, "stun", stunStateData);
        deadState = new Tank_DeadState(this, StateMachine, "dead", deadStateData);

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

    public void EnableHitbox() => hitbox.enabled = true;

    public void DisableHitbox()
    {
        damaged = false;
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damaged = true;
            other.GetComponent<IDamageable>()?.TakeDamage(enemyBaseData.damage);
        }
    }

    public override void AttackAnimationFinishTrigger()
    {
        meleeAttackState.AttackFinished();
    }
}
