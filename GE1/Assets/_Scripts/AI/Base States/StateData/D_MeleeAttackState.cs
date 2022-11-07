using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class D_MeleeAttackState : ScriptableObject
{
    public float attackCD = 2f;
}
