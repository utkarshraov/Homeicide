using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private List<Spawner> spawners = new List<Spawner>();

    [SerializeField]
    private int maxUnits;

    [SerializeField]
    private GameObject player;

    private int currentEnemies;

    private int min;

    private float[] distanceToPlayers;

    [SerializeField]
    private GameObject boss;

    void Start()
    {
        foreach (Spawner sp in FindObjectsOfType<Spawner>())
            spawners.Add(sp);
        distanceToPlayers = new float[spawners.Count];

        StartCoroutine(spawnEnemies());
        StartCoroutine(spawnBoss());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private IEnumerator spawnBoss()
    {
        yield return new WaitForSeconds(60);
        GameObject newBoss = Instantiate(boss);
        newBoss.transform.position = player.transform.position + Vector3.right * 50;
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
            yield return new WaitForSeconds(2.0f);
        }
    }

}
