using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

 void Update()
    {
        if (Input.anyKeyDown) { }
            SceneManager.LoadScene("Prototype0.1");
    }
}
