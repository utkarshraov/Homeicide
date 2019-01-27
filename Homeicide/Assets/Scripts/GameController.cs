﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private Spawner[] spawners;

    [SerializeField]
    private int maxUnits;

    [SerializeField]
    private GameObject player;

    private int currentEnemies;

    private int min;

    private float[] distanceToPlayers;

    void Start()
    {
        distanceToPlayers = new float[spawners.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemies < maxUnits)
        {
            spawnEnemy();
        }
    }

    void spawnEnemy()
    {
        calculateDistances();
        spawners[min].Spawn();
        currentEnemies++;
    }


    void calculateDistances()
    {
        int index = 0;
        min = 0;
        foreach (Spawner spawner in spawners)
        {
            Vector2 delta = spawner.transform.position - player.transform.position;
            float distance = delta.magnitude;
            distanceToPlayers[index] = distance;
            if (distanceToPlayers[index] < distanceToPlayers[min])
                min = index;
            index++;
        }
    }

    private IEnumerator spawnEnemies()
    {
        while(currentEnemies < maxUnits)
        {
            spawnEnemy();
            yield return new WaitForSeconds(2f);
        }
    }

}
