using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected FiniteStateMachine stateMachine;
    protected Enemy enemy;

    protected string animBoolName;

    public float startTime { get; protected set; }
    
    public EnemyState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName) //constructor
    {
        this.enemy = enemy; //this. since both the variable and the parameter have the same name
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        enemy.anim.SetBool(animBoolName, true);
        DoChecks();
    }
    
    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate() //Called every frame
    {

    }

    public virtual void PhysicsUpdate() //Called in fixed update
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}
