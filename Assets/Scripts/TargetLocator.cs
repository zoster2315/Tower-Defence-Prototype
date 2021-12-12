using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform headTower;
    Transform target;

    private void Start()
    {
        target = FindObjectOfType<EnemyMover>().transform;
    }

    private void Update()
    {
        AimWeapon();
    }

    private void AimWeapon()
    {
        headTower.LookAt(target);
    }
}
