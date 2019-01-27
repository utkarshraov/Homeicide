using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    public float health;

    [SerializeField]
    private int moveSpeed = 5;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private int jumpForce;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private Transform  presence;

    [SerializeField]
    private cameraController camera;

    [SerializeField]
    private Vector2 moveOffset;

    [SerializeField]
    private GameObject flamethrowerPrefab;
    [SerializeField]
    private GameObject carPrefab;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float flamethrowerDuration;

    [SerializeField]
    private AudioSource flamethrowerSound;

    [SerializeField]
    private AudioSource stompSound;

    [SerializeField]
    private AudioClip throwSound;

    [SerializeField]
    private AudioClip car;

    private bool isOnGround = true;

    [SerializeField]
    private GameObject stompBox;

    [SerializeField]
    private float shootCD;

    [SerializeField]
    private float flamethrowerCD;

    [SerializeField]
    private float shidCD;

    [SerializeField]
    private GameObject[] pointers = new GameObject[2];


    public enum Direction { Left, Right }

    public Direction Facing = Direction.Right;

    private bool lockDirection = false;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        bool oldGround = isOnGround;
        isOnGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")); // check for being on the ground
        if (oldGround == false && isOnGround == true)
        {
            stompSound.Play();
            camera.addShake(0.2f); //shake the camera if we just hit the ground
        }
        updateCooldowns();

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        UpdateDirection();

        if ((Input.GetKeyDown(KeyCode.UpArrow) && isOnGround))
        {
            //jump
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //StartCoroutine(jump());
            isOnGround = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && shootCD == 0)
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
        if (Input.GetKeyDown(KeyCode.Z) && flamethrowerCD == 0)
        {
            StartCoroutine(Flamethrower());
        }
        if (Input.GetKeyDown(KeyCode.X) && shidCD == 0 && rb.velocity.x <0.1f)
        {
            StartCoroutine(shidded());
        }

        anim.SetBool("Car", Input.GetKeyDown(KeyCode.X));
        anim.SetBool("Jump", !isOnGround);
        anim.SetBool("Walk", Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f);
        anim.SetBool("Fire", (flamethrowerCD - 5f) > 0f);

    }

    private IEnumerator jump()
    {
        float totalRotate = 0;
        while (totalRotate < 720)
        {
            transform.Rotate(0, 0, 30);
            totalRotate+=30;
            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }

    void updateCooldowns()
    {
        shootCD -= Time.deltaTime;
        flamethrowerCD -= Time.deltaTime;
        shidCD -= Time.deltaTime;
        if (shootCD < 0)
            shootCD = 0;
        if (flamethrowerCD < 0)
            flamethrowerCD = 0;
        if (shidCD < 0)
            shidCD = 0;
    }

    void UpdateDirection()
    {
        if (!lockDirection)
        {
            if (rb.velocity.x > 0)
            {
                Facing = Direction.Right;
                //presence.localScale = Vector3.one - (Vector3.right * 2f);
                presence.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (rb.velocity.x < 0)
            {
                Facing = Direction.Left;
                presence.transform.rotation = Quaternion.Euler(0, 0, 0);
                //presence.localScale = Vector3.one;
            }
        }
    }

    public void TakeDamage(float damageValue)
    {
        health -= damageValue;
        camera.addShake(0.1f);
    }


    private IEnumerator Flamethrower()
    {
        yield return new WaitForSeconds(0.4f); // animation time to setup chimney
        flamethrowerCD = 10;
        GameObject flame = Instantiate(flamethrowerPrefab);
        flamethrowerSound.Play();
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
        flamethrowerSound.Stop();
        lockDirection = false;
        moveSpeed = 15;

        flame.GetComponent<ParticleSystem>().Stop();

        yield return null;
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab);
        int direction = 1;
        stompSound.PlayOneShot(throwSound);
        if(Facing == Direction.Left)
        {
            direction = -1;
        }
        else if (Facing == Direction.Right)
        {
            direction = 1;
        }
        projectile.transform.position = transform.position + new Vector3(moveOffset.x * direction, moveOffset.y);
        shootCD = 0.5f;
        projectile.GetComponent<Projectile>().Initialise(new Vector2(moveOffset.x * direction, 0));
    }

    void checkDeath()
    {
        if(health < 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<LoadScene>().LoadAScene(2);
    }


    private IEnumerator shidded()
    {
        //yield return new WaitForSeconds(0.5f);
        GameObject projectile = Instantiate(carPrefab);
        int direction = 1;
        stompSound.PlayOneShot(car);
        if (Facing == Direction.Left)
        {
            direction = -1;
        }
        else if (Facing == Direction.Right)
        {
            direction = 1;
        }
        projectile.transform.position = transform.position + new Vector3(moveOffset.x * direction, moveOffset.y);
        shootCD = 0.5f;
        projectile.GetComponent<Projectile>().Initialise(new Vector2(moveOffset.x * direction, 0));
        shidCD = 10;
        yield return null;
    }
}
