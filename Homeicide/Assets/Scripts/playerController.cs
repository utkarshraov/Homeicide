using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private int moveSpeed = 5;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private int jumpForce;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private cameraController camera;

    [SerializeField]
    private GameObject flamethrowerPrefab;

    private bool isOnGround = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool oldGround = isOnGround;
        isOnGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")); // check for being on the ground

        if (oldGround == false && isOnGround == true)
            camera.addShake(0.2f); //shake the camera if we just hit the ground


        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            //jump
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }

        if (rb.velocity.y < 0)
        {
            //make the fall better
            rb.velocity += Vector2.up * Physics.gravity.y * 0.1f;

        }
    }

    public void TakeDamage(float damageValue)
    {
        health -= damageValue;
    }


    private IEnumerator Flamethrower()
    {
        yield return new WaitForSeconds(0.4f);




        yield return null;
    }
}
