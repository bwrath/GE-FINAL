using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        if (GetComponent<SphereCollider>() != null) damage *= 3; //headshot

        GetComponentInParent<Enemy>()?.TakeDamage(damage);
    }
}
