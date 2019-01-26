using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_player : MonoBehaviour
{
    [SerializeField] float damage;
    Collider2D atk;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerController p = collision.gameObject.GetComponent<playerController>();
        if (p)
        {
            p.TakeDamage(damage);
            atk.enabled = false;
        }
    }


}
