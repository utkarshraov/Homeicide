using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifetime : MonoBehaviour
{
    [SerializeField]
    private float timeToLive = 0f;
    private void Awake()
    {
        StartCoroutine(die());
    }

    private IEnumerator die()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
