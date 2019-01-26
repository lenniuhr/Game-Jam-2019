﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zooming : MonoBehaviour {

    private Camera camera;
	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                float zoomDistance = 5f * Input.GetAxis("Vertical") * Time.deltaTime;
                camera.transform.Translate(ray.direction * zoomDistance, Space.World);
    }
}
