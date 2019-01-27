using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class healthUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform image;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private playerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.localScale = new Vector3(player.health / maxHealth, 1, 1);
    }
}
