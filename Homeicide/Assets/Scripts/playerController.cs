using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private int moveSpeed = 5;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private int jumpForce;

    [SerializeField]
    private Transform groundCheck;

    private bool isOnGround = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")); // check for being on the ground
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * 0.1f;
        }
    }

    private void FixedUpdate()
    {
       
    }
}
