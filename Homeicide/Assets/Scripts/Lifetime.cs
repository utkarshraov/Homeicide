using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    [SerializeField] float lifetime = 1f;
    float timer = 0f;
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= lifetime)
            Destroy(gameObject);
    }
}
