using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float trauma;

    private float shake;

    [SerializeField]
    private float shakeDecay = 0.2f;

    [SerializeField]
    private float maxAngle;

    [SerializeField]
    private float maxOffset;

    private Quaternion actualRotation;

    void Start()
    {
        actualRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, -40f);
        Shake();
    }

    void Shake()
    {
        if (trauma < 0)
            trauma = 0;
        if (trauma > 1)
            trauma = 1;
        shake = trauma * trauma;
        trauma -= shakeDecay * Time.deltaTime;

        float angleX = maxAngle * shake * Random.Range(-1, 1);
        float angleY = maxAngle * shake * Random.Range(-1, 1);
        float angleZ = maxAngle * shake * Random.Range(-1, 1);
        float angleW = maxAngle * shake * Random.Range(-1, 1);

        //calculate translational shake
        float offsetX = maxOffset * shake * Random.Range(-1, 1);
        float offsetY = maxOffset * shake * Random.Range(-1, 1);

        transform.position += new Vector3(offsetX, offsetY, 0);
        transform.rotation = new Quaternion(actualRotation.x + angleX, actualRotation.y + angleY, actualRotation.z + angleZ, actualRotation.w + angleW);
    }

    public void addShake(float traumaValue)
    {
        trauma += traumaValue;
    }
}
