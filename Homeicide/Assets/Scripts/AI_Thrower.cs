using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Thrower : AI
{
    [SerializeField] float attackDistance = 4f, attackCooldown = 1f, projectileSpeed = 10f;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform muzzle;
    float timer = 0f;
    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetBool("Attack", false);
        timer += Time.fixedDeltaTime;
        if (awareness.target)
            if (Vector2.Distance(awareness.target.position, transform.position) > attackDistance)
                Move(Mathf.Sign(awareness.target.position.x - transform.position.x));
            else
            {
                Move(0f);
                Attack();
            }
    }

    void Attack()
    {
        if (timer > attackCooldown)
        {
            timer = 0f;
            anim.SetBool("Attack", true);
        }
    }

    public void Fire()
    {
        Instantiate(projectile, muzzle.position - muzzle.right, Quaternion.identity).GetComponent<Projectile>().Initialise(-muzzle.right * projectileSpeed);
    }

    public void EndFire()
    {
    }
}
