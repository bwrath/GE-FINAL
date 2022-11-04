using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPatrolStateData", menuName = "Data/State Data/Chase State")]
public class D_ChaseState : ScriptableObject
{
    public float ChaseSpeed;
    public float ActionRange;
    public float GiveUpRange;
}
