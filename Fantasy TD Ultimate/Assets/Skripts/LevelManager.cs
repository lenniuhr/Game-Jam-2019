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

    public AudioSource Audio;

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
        SpellCastingScript.Initialize();
        PlayerMode[1].SetActive(true);
        SpellCastingScript.enabled = true; 
        ActivateEnemies(wavesize/4);
        Audio.Play();
        LokalTimer = WaveTimer;
        StartTimer = true;

 
    }

    public void CreateAttakWave()
    {
        int Enemietype = 0;
        EnemieWave = new GameObject[wavesize];

        for (int i = 0; i < EnemieWave.Length; i++)
        {
            int RandomValue = Random.Range(0, 100);
            Debug.Log("Instantiate");
            if (Level == 0)
            {
                Enemietype = 0;
            }else if (Level == 1)
            {
                if (RandomValue <= 80)
                    Enemietype = 0;
                else
                    Enemietype = 1;
            }else if (Level == 2)
                {
            if (RandomValue <= 60)
                Enemietype = 0;
            else
                Enemietype = 1;
            }
            else if (Level == 3)
            {
                if (RandomValue <= 50)
                    Enemietype = 0;
                else if (RandomValue <= 90)
                    Enemietype = 1;
                else if (RandomValue <= 100)
                    Enemietype = 2;
            }
            else if (Level == 4)
            {
                if (RandomValue <= 40)
                    Enemietype = 0;
                else if (RandomValue <= 80)
                    Enemietype = 1;
                else if (RandomValue <= 100)
                    Enemietype = 2;
            }
            else if (Level == 5)
            {
                if (RandomValue <= 40)
                    Enemietype = 0;
                else if (RandomValue <= 80)
                    Enemietype = 1;
                else if (RandomValue <= 90)
                    Enemietype = 2;
                else if (RandomValue <= 100)
                    Enemietype = 3;
            }
            else
            {
                if (RandomValue <= 30)
                    Enemietype = 0;
                else if (RandomValue <= 80)
                    Enemietype = 1;
                else if (RandomValue <= 90)
                    Enemietype = 2;
                else if (RandomValue <= 100)
                    Enemietype = 3;
            }

            EnemieWave[i] =(GameObject) Instantiate(Enemies[Enemietype]) ;
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
        SpellCastingScript.Initialize();
        PlayerMode[1].SetActive(false);
        SpellCastingScript.enabled = false;
        PlayerMode[0].SetActive(true);
        BuildingSelection.enabled = true;
        BuildingSelection.BuildCap = 1;
        Level++;

        GameObject[] Deleteable;
        Deleteable = GameObject.FindGameObjectsWithTag("Finish");
        for(int i = 0; i< Deleteable.Length; i++)
        {
            Destroy(Deleteable[i]);
        }

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
