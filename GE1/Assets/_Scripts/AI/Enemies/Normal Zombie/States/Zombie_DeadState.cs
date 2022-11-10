using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_DeadState : EnemyDeadState
{
    public Zombie_DeadState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
    }
}
