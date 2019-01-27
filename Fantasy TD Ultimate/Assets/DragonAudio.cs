﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAudio : MonoBehaviour {

    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayAudio()
    {
        audioSource.Play();
    }
}
