using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerHealthBar : MonoBehaviour {

    GameObject door;
    Attacker attacker;
    Transform balken;
    float startSize;
    int startHealth;

	// Use this for initialization
	void Start () {
        door = GameObject.Find("Door");
        attacker = GetComponentInParent<Attacker>();
        balken = transform.Find("Balken");
        startSize = balken.transform.localScale.z;
        startHealth = attacker.GetHealth();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateBalken();
        RotateHealthBar();
	}

    void UpdateBalken()
    {
        float scale = (float)attacker.GetHealth() / startHealth;
        balken.transform.localScale = new Vector3(balken.transform.localScale.x, balken.transform.localScale.y, scale * startSize);


    }

    void RotateHealthBar()
    {
        transform.LookAt(door.transform.position + new Vector3(0f,20f,0f));
    }
}
