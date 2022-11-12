using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask levelMask;

    private int projectileDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>()?.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == levelMask)
            Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        projectileDamage = damage;
    }
}
