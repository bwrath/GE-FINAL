using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]
public class D_RangedAttackState : ScriptableObject
{
    public GameObject projectile;
    public float AttackRange = 8f;
    public float RangedAttackCD = 3f;
    public float RangedAttackPrepareTime = 2f;
    public float ProjectileForwardForce = 5f;
    public float ProjectileUpwardForce = 2f;
    public bool AffectedByGravity;
}
