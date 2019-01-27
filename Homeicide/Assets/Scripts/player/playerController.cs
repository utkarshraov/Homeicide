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
    private Vector2 moveOffset;

    [SerializeField]
    private GameObject flamethrowerPrefab;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float flamethrowerDuration;

    private bool isOnGround = true;

    [SerializeField]
    private GameObject stompBox;

    [SerializeField]
    private float shootCD;

    [SerializeField]
    private float flamethrowerCD;

    [SerializeField]
    private GameObject[] pointers = new GameObject[2];


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

        if ((Input.GetKeyDown(KeyCode.UpArrow) && isOnGround))
        {
            //jump
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (rb.velocity.y < 0)
        {
            //make the fall better
            rb.velocity += Vector2.up * Physics.gravity.y * 0.1f;
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Flamethrower());
        }
        updateCooldowns();
    }

    void updateCooldowns()
    {
        shootCD -= Time.deltaTime;
        flamethrowerCD -= Time.deltaTime;
        if (shootCD < 0)
            shootCD = 0;
        if (flamethrowerCD < 0)
            flamethrowerCD = 0;
    }

    void UpdateDirection()
    {
        if (!lockDirection)
        {
            if (rb.velocity.x > 0)
            {
                Facing = Direction.Right;
                pointer.transform.position = pointers[0].transform.position;
            }
            else if (rb.velocity.x < 0)
            {
                Facing = Direction.Left;
                pointer.transform.position = pointers[1].transform.position;
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
        {
            flame.transform.position = transform.position + new Vector3(-moveOffset.x, moveOffset.y);
            flame.transform.Rotate(Vector3.forward, 185);
        }
        else if (Facing == Direction.Right)
            flame.transform.position = transform.position + new Vector3(moveOffset.x, moveOffset.y);
        flame.transform.SetParent(transform);

        //flame.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        lockDirection = true;
        moveSpeed = 10;

        yield return new WaitForSeconds(flamethrowerDuration);

        lockDirection = false;
        moveSpeed = 15;

        flame.GetComponent<ParticleSystem>().Stop();

        yield return null;
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab);
        int direction = 1;
        if(Facing == Direction.Left)
        {
            direction = -1;
        }
        else if (Facing == Direction.Right)
        {
            direction = 1;
        }
        projectile.transform.position = transform.position + new Vector3(moveOffset.x * direction, moveOffset.y);

        projectile.GetComponent<Projectile>().Initialise(new Vector2(moveOffset.x * direction, 0));
    }
}
