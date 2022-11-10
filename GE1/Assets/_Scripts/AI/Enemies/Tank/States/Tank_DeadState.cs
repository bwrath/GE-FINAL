using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_DeadState : EnemyDeadState
{
    public Tank_DeadState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
    }

    
}
