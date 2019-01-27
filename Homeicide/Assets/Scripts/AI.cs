using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] protected float health = 10f, walkSpeed = 3f, jumpSpeed = 4f, attackCooldown = 2f, attackDistance = 4f;
    [SerializeField] protected GameObject guts;
    [SerializeField] protected Transform eject;
    protected float timer;
    protected Collider2D foot;
    protected Awareness awareness;
    protected Rigidbody2D body;
    protected Animator anim;
    protected bool dead = false;

    // Start is called before the first frame update
    void Awake()
    {
        foot = GetComponent<Collider2D>();
        awareness = GetComponentInChildren<Awareness>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        anim.SetBool("Attack", false);
        if (!dead)
        {
            if (awareness.target)
            {
                float control = Mathf.Sign(awareness.target.position.x - transform.position.x);
                Look(control);
                if (Vector2.Distance(awareness.target.position, transform.position) > attackDistance)
                    Move(control);
                else
                {
                    Move(0f);
                    Attack();
                }
            }
        }
        else
        {
            Move(0f);
            if (timer > attackCooldown)
                Destroy(gameObject);
        }
    }

    protected void Attack()
    {
        if (timer > attackCooldown)
        {
            timer = 0f;
            anim.SetBool("Attack", true);
        }
    }

    public void Damage(float i_dam)
    {
        health -= i_dam;
        if (health <= 0f)
            Death();
    }

    public void Death()
    {
        timer = 0f;
        Instantiate(guts, eject.position, eject.rotation);
        anim.SetBool("Dead", dead = true);
        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
            col.enabled = false;
        body.bodyType = RigidbodyType2D.Kinematic;
    }

    protected void Move(float i_move)
    {
        if (foot.IsTouchingLayers())
        {
            anim.SetBool("Walk", i_move != 0f);
            body.velocity = new Vector2( walkSpeed * i_move, body.velocity.y);
        }
        else
            anim.SetBool("Walk", false);
    }

    protected void Look(float i_look)
    {
        if (i_look != 0f)
            transform.localScale = new Vector3(-i_look, transform.localScale.y, transform.localScale.z);
    }

    protected void Jump()
    {
        if(foot.IsTouchingLayers())
            body.velocity += Vector2.up * jumpSpeed;
    }
}
