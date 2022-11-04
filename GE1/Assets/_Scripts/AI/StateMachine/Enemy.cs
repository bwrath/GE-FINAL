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

    //Assignables
    [Header("Assignables")]
    public Vector3[] Waypoints;

    //Settings
    [Header("Settings")]
    //Field of View
    [Header("Field Of View")]
    [SerializeField] private float viewRadius;
    [SerializeField] private float viewAngle;

    //Variables
    public GameObject player;
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
    }

    protected virtual void Start()
    {
        gameStarted = true;
        originalPosition = transform.position;
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
