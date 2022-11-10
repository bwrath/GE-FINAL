using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyBaseData", menuName = "Data/Base Data/Enemy Base Data")]
public class D_EnemyBase : ScriptableObject
{
    public float wallCheckDistance = 2f;
    public float ledgeCheckDistance = 1f;

    public LayerMask levelMask;
}
