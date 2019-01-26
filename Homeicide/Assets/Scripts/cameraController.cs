using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position - Vector3.forward * 20.0f + new Vector3(player.GetComponent<Rigidbody2D>().velocity.x * 2,0,0);
    }
}
