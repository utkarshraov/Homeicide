using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Charger : AI
{
    [SerializeField] Collider2D atk;

    public void Fire()
    {
        atk.enabled = true;
    }

    public void EndFire()
    {
        atk.enabled = false;
    }
}
