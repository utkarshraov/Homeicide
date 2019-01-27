using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Thrower : AI
{
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform muzzle;

    public void Fire()
    {
        Instantiate(projectile, muzzle.position - muzzle.right, Quaternion.identity).GetComponent<Projectile>().Initialise(-muzzle.right * projectileSpeed);
    }

    public void EndFire()
    {
    }
}
