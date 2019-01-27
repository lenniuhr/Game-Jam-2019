using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo: add a generic element all enemies inherit from
//todo: make all enemies have a StartWave element which is the first wave they can spawn in
//todo: give all Enemies a int called 'Difficulty'
//todo: make enemies get damage when round ends

//todo: Make the enemy amount deciding smarter


//We have to use a multidimensional entry because we have n lanes with m entitys
public class LaneListEntryKV
{
    public int lanenumber; // key
    public int queuedenemies; //value2
    public List<GameObject> enemies; // value
}

public class EnemySpawnerV2 : MonoBehaviour
{

    //Spawn a burst of enemies every x seconds
    public float SpawnDelay = 10;

    //This is the maximum amount of enemies per lane overall
    public int LaneMaxEnemies = 300;

    //The maximum amount of enemies
    public int MaxEnemies = 2000;//LaneMaxEnemies * SpawnPoints.Count;

    //The next time when we should spawn a burst of enemies
    public double NextBurstSpawnTime = 0;

    //The Enemy we have
    public List<GameObject> EnemyPrefabs = new List<GameObject>();

    //Time that has to pass between each spawn (wait this many seconds after each enemy spawn before spawning a new one in the lane)
    public float TimeWaitAfterSpawnEnemy = 1.0f;

    //The amount of enemies which should enter each lane in this round
    public int EnemiesPerLaneThisRound = 10;

    //How many enemies are in each lane
    private List<LaneListEntryKV> LaneEnemies = new List<LaneListEntryKV>();



    //Our spawnpoints/lanes
    private List<GameObject> SpawnPoints;

    private WaveController waveControl;
    // used lane to spawn shit, keeps incrementing and going back to 0
    int spawnlane = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPoints = new List<GameObject>();
        GameObject obj = GameObject.Find("WaveController");
        if (obj == null)
        {
            Debug.Log("[EnemySpawner] Could not find WaveController!");
        }
        else
        {
            waveControl = obj.GetComponent<WaveController>();
        }



    }

    //Returns the amount of enemies in this lane
    int GetLaneEnemyCount(int lane, bool onlyactive = false)
    {
        for (int i = 0; i < LaneEnemies.Count; i++)
        {
            LaneListEntryKV kv = LaneEnemies[i];
            if (kv.lanenumber == i)
            {
                if (!onlyactive)
                    return kv.enemies.Count + kv.queuedenemies;

                return kv.enemies.Count;
            }
        }

        Debug.Log("[EnemySpawner] Could not get lane count: " + lane + "!");

        return -1;
    }

    //Returns true if this SPECIFIC lane is full for this round
    bool IsLaneFullForRound(int lane)
    {
        if (GetLaneEnemyCount(lane) > EnemiesPerLaneThisRound)
        {
            //We still have room, return false
            return true;
        }

        return false;
    }

    //Returns true if all lanes are full for this round
    bool AllLanesFullForRound()
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            if (!IsLaneFullForRound(i))
            {
                return false;
            }
        }
        return true;
    }

    //Returns true if this lane is full because of maximum lane enemy count
    bool IsLaneFullMax(int lane)
    {
        if (GetLaneEnemyCount(lane) < LaneMaxEnemies)
        {
            //We still have room, return false
            return false;
        }

        return true;
    }

    //Returns true if all lanes are full because of maximum lane enemy count
    bool AllLanesFullMax()
    {


        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            if (!IsLaneFullMax(i))
            {
                //We still have room, return false
                return false;
            }
        }

        return true;
    }

    //Returns a specific LaneListEntryKV
    LaneListEntryKV GetLaneEntry(int lanenum)
    {
        for (int i = 0; i < LaneEnemies.Count; i++)
        {
            LaneListEntryKV curkv = LaneEnemies[i];
            if (curkv.lanenumber == lanenum)
                return curkv;
        }

        return null;
    }

    //Kills all people in a lane and clean our list
    void KillLaneEnemies(int lanenum)
    {
        if (GetLaneEnemyCount(lanenum) == 0)
            return;


        LaneListEntryKV kv = GetLaneEntry(lanenum);

        for (int i = 0; i < kv.enemies.Count; i++)
        {
            GameObject enemy = kv.enemies[i];
            kv.enemies.Remove(enemy);
        }

        //remake the list
        kv.queuedenemies = 0; // todo: remove every coroutine we created

        Debug.Log("CLEARED LANE: " + GetLaneEnemyCount(lanenum));

    }
    // Update is called once per frame
    void Update()
    {
        //We need to wait until we have spawn points


        if (SpawnPoints.Count == 0)
        {


            int lanenum = 0;

            foreach (Transform child in transform)
            {
                //Ignore ourselves
                if (child == transform)
                    continue;

                SpawnPoints.Add(child.gameObject);

                LaneListEntryKV newkv = new LaneListEntryKV();
                newkv.lanenumber = lanenum;
                newkv.enemies = new List<GameObject>();
                newkv.queuedenemies = 0;

                LaneEnemies.Add(newkv);

                Debug.Log("[EnemySpawner] Added spawnpoint " + child.name + " lane num: " + newkv.lanenumber);
                lanenum += 1;
            }

            if (LaneEnemies.Count == 0)
            {
                Debug.Log("[EnemySpawner] No spawn points!");
            }



            return;
        }

        if(waveControl.InPause)
        {
            EnemiesPerLaneThisRound = 6 * (waveControl.CurWave  + 1);

            for (int i=0;i<LaneEnemies.Count;i++)
            {
                KillLaneEnemies(i);
            }
            NextBurstSpawnTime = 0;
        }

        if (!waveControl.InPause && Time.time > NextBurstSpawnTime)
        {
            Debug.Log("[EnemySpawner] We should spawn now");

            //Only do this if we have any lanes
            if (IsLaneFullForRound(spawnlane))
            {
                Debug.Log("[EnemySpawner] this lane is already full");
            }

            NextBurstSpawnTime = Time.time + SpawnDelay;


            int laneenemies = GetLaneEnemyCount(spawnlane);
            if(EnemiesPerLaneThisRound > laneenemies)
            {
                int curatonce = EnemiesPerLaneThisRound - laneenemies;

                //Make sure we are trying to spawn enough enemies
               

                int howmany = Random.Range(1, curatonce);

                if (curatonce > 6)
                {
                    curatonce = Random.Range(1, curatonce / 3);
                }

                Debug.Log("[EnemySpawner] Spawning " + howmany + " enemies!");
                SpawnBurst(spawnlane, howmany, EnemyPrefabs);
            }



            spawnlane++;


            if (spawnlane > SpawnPoints.Count - 1)
            {
                spawnlane = 0;
            }

        }
        else
        {
            //Do we even have any enemies right now
            if (LaneEnemies.Count != 0)
            {
                //Update the statuses for the enemies
                for (int i = 0; i < LaneEnemies.Count; i++)
                {
                    UpdateLaneEnemies(GetLaneEntry(i));
                }
            }

        }
    }

    //Checks whether a specific gameObject is alive and well for us
    bool ValidLaneEnemyStatus(GameObject enemy)
    {
        if (!enemy)
            return false;

        //todo: Add healthcheck here with return false

        return true;
    }

    //This is called to check the statuses of the enemies in each lane (e.g. whether they died or got deleted)
    //todo: Fix
    void UpdateLaneEnemies(LaneListEntryKV lanekv)
    {


        for (int i = 0; i < lanekv.enemies.Count; i++)
        {
            if (ValidLaneEnemyStatus(lanekv.enemies[i]))
                continue;

            lanekv.enemies.Remove(lanekv.enemies[i]);
        }
    }

    //This is called to spawn a burst of enemies and to appreciate to the max enemy value
    void SpawnBurst(int lane, int amount, List<GameObject> allowedtypes)
    {

        int laneenemies = GetLaneEnemyCount(lane);

        if (laneenemies > LaneMaxEnemies) // lane already has too much!
        {
            Debug.Log("[Enemiespawner] Lane: " + lane + " already has too much enemies!");
            return;
        }

        int newlaneenemies = laneenemies + amount;

        if (newlaneenemies > LaneMaxEnemies) // lane would have too much!
        {
            Debug.Log("[Enemiespawner] Lane: " + lane + " is filled to the brim!");
            newlaneenemies = LaneMaxEnemies - laneenemies; // adjust the newlaneenemies count so it doesn't go over our maximum
        }

        for (int i = 0; i < amount; i++)
        {
            //Spawn the Enemies but with a delay so they don't all spawn at once
            GetLaneEntry(lane).queuedenemies += 1;
            StartCoroutine(SpawnEnemyCourtine(lane, EnemyPrefabs[0], TimeWaitAfterSpawnEnemy * (1+i)));
        }
    }

    //This is responsible for delaying entity spawns by x seconds
    IEnumerator SpawnEnemyCourtine(int lane, GameObject enemyPrefab, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        GetLaneEntry(lane).queuedenemies -= 1;
        if (!waveControl.InPause)
        {
            SpawnEnemy(lane, enemyPrefab);
        }

        
    }

    //This is called whenever we should spawn a enemy
    void SpawnEnemy(int lane, GameObject enemyPrefab)
    {
        Transform spawnPoint = SpawnPoints[lane].transform;

        GameObject enemy = (GameObject)Instantiate(enemyPrefab);
        enemy.GetComponent<Transform>().position = spawnPoint.position;

        Attacker attacker = enemy.GetComponent<Attacker>();
        attacker.maxHealth = 100;

        LaneListEntryKV lanekv = GetLaneEntry(lane);
        lanekv.enemies.Add(enemy);
    }
}
