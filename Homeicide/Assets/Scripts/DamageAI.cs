using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAI : MonoBehaviour
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private bool destroyOnCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AI ai = collision.gameObject.GetComponent<AI>();
        if (ai)
        {
            ai.Damage(damage);
            if (destroyOnCollision)
                Destroy(gameObject);
        }
    }

}
