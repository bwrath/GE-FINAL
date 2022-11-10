using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour, IDamageable
{
    private Enemy enemy;

    public void TakeDamage(int damage)
    {
        enemy = GetComponentInParent<Enemy>();

        if (GetComponent<SphereCollider>() != null) //headshot
        {
            damage *= 3;
            if (enemy.HeadShotParticles != null)
            {
                Instantiate(enemy.HeadShotParticles, transform.position, Quaternion.identity);
            }
        }

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            if (enemy.HurtParticles != null)
            {
                Instantiate(enemy.HurtParticles, transform.position, Quaternion.identity);
            }
        }
            
    }
}
