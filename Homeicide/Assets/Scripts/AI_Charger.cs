using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Charger : AI
{
    [SerializeField] Collider2D atk;

    [SerializeField]
    private AudioClip[] screams;

    public void Fire()
    {
        atk.enabled = true;
    }

    public void EndFire()
    {
        atk.enabled = false;
    }

    public void Scream()
    {
        GetComponent<AudioSource>().Pause();
        GetComponent<AudioSource>().mute = false;
        GetComponent<AudioSource>().volume = 0.4f;
        GetComponent<AudioSource>().PlayOneShot(screams[Random.Range(0, screams.Length)]);
    }
}
