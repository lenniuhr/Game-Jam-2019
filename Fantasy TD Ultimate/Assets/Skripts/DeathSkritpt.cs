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
            Debug.Log("Game Over");
            SceneManager.LoadScene("EndScene", LoadSceneMode.Additive);
        }
    }
}
