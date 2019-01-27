using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public int Level = 0;
    public Vector3[] SpawningPoints;
    public GameObject[] PlayerMode;
    public bool BuildPhase = true;
    public GameObject[] Enemies;
    public BuildingSelectionScript BuildingSelection;
    public SpellCasting SpellCastingScript;

    public float WaveTimer = 100f;
    public float LokalTimer;
    private bool StartTimer = false;
    private int WaveCount = 3;
    private bool WaveCleared = false;

    private int MinEnemies = 20;
    private int EnemieGain = 5;
    private int wavesize = 0;
    private int WaveIndex = 0;

    public GameObject[] EnemieWave;

    public void StartWave()
    {
        WaveCount = 3;
        WaveIndex = 0;
        wavesize = MinEnemies + (Level * EnemieGain);
        PlayerMode[0].SetActive(false);
        BuildingSelection.enabled = false;
        CreateAttakWave();
        PlayerMode[1].SetActive(true);
        SpellCastingScript.enabled = true;
        SpellCastingScript.Initialize();
        ActivateEnemies(wavesize/4);
        LokalTimer = WaveTimer;
        StartTimer = true;

 
    }

    public void CreateAttakWave()
    {
        int Enemietype = 0;
        EnemieWave = new GameObject[wavesize];

        for (int i = 0; i < EnemieWave.Length; i++)
        {
            int RandomValue = 1;
            Debug.Log("Instantiate");


            EnemieWave[i] =(GameObject) Instantiate(Enemies[0]) ;
            EnemieWave[i].SetActive(false);
        }
    }

    public void ActivateEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            EnemieWave[WaveIndex].SetActive(true);
            EnemieWave[WaveIndex].GetComponent<Transform>().position = SpawningPoints[Random.Range((int)0, (int)3)];
            EnemieWave[WaveIndex].GetComponent<Transform>().position = new Vector3(EnemieWave[WaveIndex].GetComponent<Transform>().position.x+ Random.Range(-5f, 5f), EnemieWave[WaveIndex].GetComponent<Transform>().position.y, EnemieWave[WaveIndex].GetComponent<Transform>().position.z + Random.Range(-5f, 5f));
            WaveIndex++;
        }
        WaveCount--;
    }
	
    private void WaveWasCleared()
    {
        BuildPhase = true;
        PlayerMode[1].SetActive(false);
        SpellCastingScript.enabled = false;
        PlayerMode[0].SetActive(true);
        BuildingSelection.enabled = true;
        BuildingSelection.BuildCap = 3;
        Level++;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (BuildPhase)
        {

        }
        else
        {
            if (WaveCount <= 0)
            {
                StartTimer = false;
                WaveCleared = true;
                for (int i= 0; i < WaveIndex; i++)
                {
                    if (EnemieWave[i] != null) {
                        WaveCleared = false;
                    }
                    
                }
                if (WaveCleared)
                {
                    Debug.Log("Wave Gecleard");
                    WaveWasCleared();
                }
            }
            if (StartTimer) {
                if (LokalTimer > 0)
                {
                    LokalTimer-= Time.deltaTime;
                }
                else
                {
                    LokalTimer = WaveTimer;
                    if (WaveCount == 2)
                    {
                        ActivateEnemies((wavesize - WaveIndex) / 2);
                    }else if (WaveCount == 1)
                    {
                        ActivateEnemies(wavesize - WaveIndex);
                    }
                    
                }
            }

        }


	}
}
