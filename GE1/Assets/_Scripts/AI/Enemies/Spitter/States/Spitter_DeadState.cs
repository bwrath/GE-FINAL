using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter_DeadState : EnemyDeadState
{
    public Spitter_DeadState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(enemy, stateMachine, animBoolName, stateData)
    {
    }


}
