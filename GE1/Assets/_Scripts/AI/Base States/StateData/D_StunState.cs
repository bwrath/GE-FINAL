using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class D_StunState : ScriptableObject
{
    public float StunTime = 5f;
    public GameObject StunBeginEffect;
    public GameObject StunLoopEffect;
}
