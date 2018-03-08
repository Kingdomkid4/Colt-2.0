using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    private Vector3[] spawnpositions = new Vector3[] {new Vector3(0, 1, 10), new Vector3(5, 1, 10), new Vector3(10, 1, 10), new Vector3(15, 1, 10), new Vector3(20, 1, 10)};
    public GameObject enemy;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemy, spawnpositions[i], Quaternion.identity);
        }
    }
}
