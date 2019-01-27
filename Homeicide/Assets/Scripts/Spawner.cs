using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    public void Spawn()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
