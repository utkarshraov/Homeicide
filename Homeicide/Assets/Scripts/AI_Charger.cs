using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Charger : AI
{
    [SerializeField] float attackDistance, attackCooldown;
    float timer = 0f;
    // Update is called once per frame
    void FixedUpdate()
    {
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
        awareness.target.GetComponent<playerController>();
        if(timer > attackCooldown)
        {
            timer = 0f;
            anim.SetBool("Attack", true);
        }
    }
}
