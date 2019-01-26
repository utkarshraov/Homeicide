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
    private Transform pointer;

    [SerializeField]
    private cameraController camera;

    [SerializeField]
    private float flameOffset;

    [SerializeField]
    private GameObject flamethrowerPrefab;

    [SerializeField]
    private float flamethrowerDuration;

    private bool isOnGround = true;

    private enum Direction { Left, Right }

    Direction Facing = Direction.Right;

    private bool lockDirection = false;

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
        UpdateDirection();

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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Flamethrower());
        }
    }

    void UpdateDirection()
    {
        if (!lockDirection)
        {
            if (rb.velocity.x > 0)
            {
                Facing = Direction.Right;
                pointer.transform.position = new Vector3(0.5f * 8f, 0.3f * 8f) + transform.position;
            }
            else if (rb.velocity.x < 0)
            {
                Facing = Direction.Left;
                pointer.transform.position = new Vector3(-0.5f * 8f, 0.3f * 8f) + transform.position;
            }
        }
    }

    public void TakeDamage(float damageValue)
    {
        health -= damageValue;
    }


    private IEnumerator Flamethrower()
    {
        yield return new WaitForSeconds(0.4f); // animation time to setup chimney

        GameObject flame = Instantiate(flamethrowerPrefab);
        if (Facing == Direction.Left)
            flame.transform.position = transform.position + new Vector3(-flameOffset, 0);
        else if (Facing == Direction.Right)
            flame.transform.position = transform.position + new Vector3(flameOffset, 0);
        flame.transform.SetParent(transform);
        lockDirection = true;

        yield return new WaitForSeconds(flamethrowerDuration);

        lockDirection = false;

        Destroy(flame);

        yield return null;
    }
}
