using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float duration;

    [SerializeField]
    private Vector2 direction;

    [SerializeField]
    Rigidbody2D rb;

    private void Start()
    {
        StartCoroutine(DeathTimer());
    }

    public void Initialise(Vector2 newDirection)
    {
        direction = newDirection;
        rb.velocity = newDirection * speed;
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
        yield return null;
    }

}
