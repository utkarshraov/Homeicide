﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAI : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private void OnCollisionStay2D(Collision2D collision)
    {
        AI ai = collision.gameObject.GetComponent<AI>();
        if(ai)
        {
            ai.Damage(damage);
        }
    }

}