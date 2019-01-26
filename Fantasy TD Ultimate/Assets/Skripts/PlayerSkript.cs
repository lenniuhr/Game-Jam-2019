using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkript : MonoBehaviour {

    public float Mana = 100f;
    public float Credits = 100f;
    public GameObject ManaBar;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Mana < 100)
        {
            Mana += Time.deltaTime;
        }
        ManaBar.GetComponent<Transform>().localScale = new Vector3(Mana/100f, 1f, 1f);

    }
}
