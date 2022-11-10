using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour, IDamageable
{
    private Enemy enemy;

    public void TakeDamage(int damage)
    {
        if (GetComponent<SphereCollider>() != null) damage *= 3; //headshot

        enemy = GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            if (enemy.HurtParticles != null)
            {
                Instantiate(enemy.HurtParticles, transform.position, transform.rotation);
            }
        }
            
    }
}
