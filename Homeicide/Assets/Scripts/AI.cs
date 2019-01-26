using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] protected float health, walkSpeed, jumpSpeed;
    [SerializeField] protected GameObject guts;
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

    public void Damage(float i_dam)
    {
        health -= i_dam;
        if (health <= 0f)
            Death();
    }

    public void Death()
    {
        Instantiate(guts, transform.position, transform.rotation);
        anim.SetBool("Dead", true);
        Destroy(this);
    }

    protected void Move(float i_move)
    {
        if (foot.IsTouchingLayers())
        {
            if (i_move != 0f)
                transform.localScale = new Vector3(-i_move, transform.localScale.y, transform.localScale.z);
            anim.SetBool("Walk", i_move != 0f);
            body.velocity = new Vector2( walkSpeed * i_move, body.velocity.y);
        }
        else
            anim.SetBool("Walk", false);
    }

    protected void Jump()
    {
        if(foot.IsTouchingLayers())
            body.velocity += Vector2.up * jumpSpeed;
    }
}
