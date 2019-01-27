using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSkritpt : MonoBehaviour {
    public Defender skript;
    
    public void update()
    {
        if (skript.health <= 0)
        {
            SceneManager.LoadScene("EndScene", LoadSceneMode.Additive);
        }
    }
}
