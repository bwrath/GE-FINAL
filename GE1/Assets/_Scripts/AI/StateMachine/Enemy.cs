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

    //Assignables
    [Header("Assignables")]
    public Vector3[] Waypoints;

    [Header("Settings")]
    [Header("Navigation")]
    public bool RandomizeWaypoints;
    public float WaypointRange;

    //Variables
    public Vector3 originalPosition { get; private set; }
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
            for (int i = 0; i < Waypoints.Length; i++)
            {
                randomX = Random.Range(-WaypointRange, WaypointRange);
                randomZ = Random.Range(-WaypointRange, WaypointRange);
                Waypoints[i] = new Vector3(randomX, 0f, randomZ);
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
        if (currentWaypoint >= Waypoints.Length)
            currentWaypoint = 0;
    }

    public virtual bool PlayerInView => fov.visibleTargets.Count != 0;

    public virtual void SetSpeed(float speed) => navMeshAgent.speed = speed;

    public virtual void SetDestination(Vector3 destination) => navMeshAgent.SetDestination(destination);

    public virtual void SetStoppingDistance(float distance) => navMeshAgent.stoppingDistance = distance;

    private void OnDrawGizmos()
    {
        if (!gameStarted) originalPosition = transform.position;

        if (Waypoints != null)
        {
            for (int i = 0; i < Waypoints.Length; i++)
            {
                // Draw points
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(originalPosition + Waypoints[i], 0.4f);

                // Draw lines
                Gizmos.color = Color.black;
                if (i < Waypoints.Length)
                {
                    Gizmos.DrawLine(originalPosition + Waypoints[i], originalPosition + Waypoints[(i + 1) % Waypoints.Length]);
                }
            }
        }
    }
}
