using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_player : MonoBehaviour
{
    [SerializeField] float damage;
    Collider2D atk;

    private void Awake()
    {
        atk = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        playerController p = col.gameObject.GetComponent<playerController>();
        if (p)
        {
            p.TakeDamage(damage);
            atk.enabled = false;
        }
    }


}
