using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform headTower;
    [SerializeField] float range = 15;
    [SerializeField] ParticleSystem projectileParticles;
    Transform target;

    private void Update()
    {
        //if (target == null || !target.gameObject.activeSelf)
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDidstnce = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDidstnce < maxDistance)
            {
                maxDistance = targetDidstnce;
                closestTarget = enemy.transform;
            }
        }
        target = closestTarget;
    }

    private void AimWeapon()
    {
        if (target == null)
            return;
        float targetDistance = Vector3.Distance(transform.position, target.position);
        Attack(targetDistance <= range);

        headTower.LookAt(target);
    }

    void Attack(bool isActive)
    {
        ParticleSystem.EmissionModule emission = projectileParticles.emission;
        emission.enabled = isActive;
    }
}
