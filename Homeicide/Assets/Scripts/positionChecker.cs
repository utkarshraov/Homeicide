using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionChecker : MonoBehaviour
{
    [SerializeField]
    private float maxLeftDistance;

    [SerializeField]
    private float maxRightDistance;

    private void Update()
    {
        if (transform.position.x > maxRightDistance)
            transform.position = new Vector3(maxLeftDistance + 1, transform.position.y);
        if (transform.position.x < maxLeftDistance)
            transform.position = new Vector3(maxRightDistance - 1, transform.position.y);
    }
}
