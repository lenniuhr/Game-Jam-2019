using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemies = new GameObject[1];

    public int[] amounts = new int[1];

    public Vector3[] spawnPoints = new Vector3[3];

    private GameObject attackers;

	// Use this for initialization
	void Start () {
        attackers = GameObject.Find("Attackers");
        if(!attackers)
        {
            attackers = Instantiate(new GameObject("Attackers"));
        }

        foreach (Vector3 spawnPoint in spawnPoints)
        {
            for(int i = 0; i < enemies.Length; i++)
            {
                for(int j = 0; j < amounts[i]; j++)
                {
                    GameObject enemy = Instantiate(enemies[i], attackers.transform);
                    enemy.transform.Translate(spawnPoint);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
