using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPatrolStateData", menuName = "Data/State Data/Patrol State")]
public class D_PatrolState : ScriptableObject
{
    public float PatrolSpeed = 3.5f;
    public float StoppingDistance = 1f;
}
