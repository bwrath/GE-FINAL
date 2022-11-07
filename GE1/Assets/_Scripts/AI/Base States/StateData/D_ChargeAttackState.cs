using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChargeAttackData", menuName = "Data/State Data/Charge Attack State")]
public class D_ChargeAttackState : ScriptableObject
{
    public float ChargeAttackCD = 5f;
    public float ChargePrepareTime = 2f;
    public float ChargeSpeed = 20f;
    public float MaxChargeDuration = 2f;
}
