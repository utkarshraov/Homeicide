using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] protected float health, walkSpeed, jumpSpeed;
    protected Collider2D foot;
    protected Awareness awareness;
    protected Rigidbody2D body;
    protected Animator anim;

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
