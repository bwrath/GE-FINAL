using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //Dependencies
    public FiniteStateMachine StateMachine { get; private set; }
    public NavMeshAgent navMeshAgent { get; private set; }
    public Animator anim { get; protected set; }
    public FieldOfView fov { get; private set; }
    private Rigidbody[] ragdoll;

    [Header("Base Data")]
    [SerializeField] protected D_EnemyBase enemyBaseData;

    //Assignables
    [Header("Assignables")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Vector3[] waypoints;
    [SerializeField] private GameObject hurtParticles;
    [SerializeField] private GameObject headShotParticles;

    [Header("Settings")]
    [Header("Navigation")]
    public bool RandomizeWaypoints;
    public float WaypointRange;

    //Variables and Properties
    public D_EnemyBase EnemyBaseData => enemyBaseData;
    public int currentHealth { get; private set; }
    public GameObject HurtParticles => hurtParticles;
    public GameObject HeadShotParticles => headShotParticles;
    public Vector3 originalPosition { get; private set; }
    public Vector3[] Waypoints => waypoints;
    public int currentWaypoint { get; private set; }

    private bool gameStarted = false;
    private Vector3 currentVelocity;

    protected Transform player;

    //Methods
    protected virtual void Awake()
    {
        StateMachine = new FiniteStateMachine();
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
        ragdoll = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(false);
        foreach (var rigidbody in ragdoll)
        {
            rigidbody.gameObject.AddComponent<EnemyHurtbox>();
        }
    }

    protected virtual void Start()
    {
        gameStarted = true;
        originalPosition = transform.position;
        currentHealth = enemyBaseData.maxHealth;
        player = GameObject.Find("Player").transform;

        if (RandomizeWaypoints)
        {
            float randomX, randomZ;
            for (int i = 0; i < waypoints.Length; i++)
            {
                randomX = Random.Range(-WaypointRange, WaypointRange);
                randomZ = Random.Range(-WaypointRange, WaypointRange);
                waypoints[i] = new Vector3(randomX, 0f, randomZ);
            }
        }
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();

        currentVelocity = navMeshAgent.velocity;
    }

    protected virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void WaypointReached()
    {
        currentWaypoint++;
        if (currentWaypoint >= waypoints.Length)
            currentWaypoint = 0;
    }

    public virtual bool DetectingWall => Physics.Raycast(wallCheck.position, transform.forward, enemyBaseData.wallCheckDistance, enemyBaseData.levelMask);

    public virtual bool DetectingLedge => !Physics.Raycast(ledgeCheck.position, Vector3.down, enemyBaseData.ledgeCheckDistance, enemyBaseData.levelMask);

    public virtual bool PlayerInView => fov.visibleTargets.Count != 0;

    public virtual void SetSpeed(float speed) => navMeshAgent.speed = speed;

    public virtual void SetDestination(Vector3 destination) => navMeshAgent.SetDestination(destination);

    public virtual void SetStoppingDistance(float distance) => navMeshAgent.stoppingDistance = distance;

    public virtual void AttackAnimationFinishTrigger() { }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public virtual void ToggleRagdoll(bool toggle)
    {
        foreach (var rigidbody in ragdoll)
        {
            rigidbody.isKinematic = !toggle;
        }
        anim.enabled = !toggle;
    }

    private void OnDrawGizmos()
    {
        //Wall and ledge checks
        Gizmos.color = Color.green;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.forward * enemyBaseData.wallCheckDistance);
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + Vector3.down * enemyBaseData.ledgeCheckDistance);

        //Waypoints
        if (!gameStarted) originalPosition = transform.position;

        if (waypoints != null)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                // Draw points
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(originalPosition + waypoints[i], 0.4f);

                // Draw lines
                Gizmos.color = Color.black;
                if (i < waypoints.Length)
                {
                    Gizmos.DrawLine(originalPosition + waypoints[i], originalPosition + waypoints[(i + 1) % waypoints.Length]);
                }
            }
        }
    }
}
