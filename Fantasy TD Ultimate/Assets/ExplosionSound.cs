using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour {

    AudioSource audioSource;
    
        // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        Invoke("PlaySound", 3.5f);
	}
	
    void PlaySound()
    {
        audioSource.Play();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
