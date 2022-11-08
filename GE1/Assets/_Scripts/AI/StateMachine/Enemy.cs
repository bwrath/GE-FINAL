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

    [Header("Base Data")]
    [SerializeField] private D_EnemyBase enemyBaseData;

    //Assignables
    [Header("Assignables")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Vector3[] waypoints;

    [Header("Settings")]
    [Header("Navigation")]
    public bool RandomizeWaypoints;
    public float WaypointRange;

    //Variables
    public Vector3 originalPosition { get; private set; }
    public Vector3[] Waypoints => waypoints;
    public int currentWaypoint { get; private set; }

    private bool gameStarted = false;
    private Vector3 currentVelocity;

    //Methods
    protected virtual void Awake()
    {
        StateMachine = new FiniteStateMachine();
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
    }

    protected virtual void Start()
    {
        gameStarted = true;
        originalPosition = transform.position;

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
