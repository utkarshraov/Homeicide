using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAIDPS : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        AI ai = collision.gameObject.GetComponent<AI>();
        if (ai)
        {
            ai.Damage(damage * Time.fixedDeltaTime);
        }
    }

}