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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(moveSpeed, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
