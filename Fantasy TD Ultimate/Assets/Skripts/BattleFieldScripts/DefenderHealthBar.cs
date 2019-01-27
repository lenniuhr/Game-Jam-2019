using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderHealthBar : MonoBehaviour {

    GameObject door;
    Defender defender;
    Transform balken;
    float startSize;
    int startHealth;

    // Use this for initialization
    void Start()
    {
        door = GameObject.Find("Door");
        defender = GetComponentInParent<Defender>();
        balken = transform.Find("Balken");
        startSize = balken.transform.localScale.z;
        startHealth = defender.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBalken();
        RotateHealthBar();
    }

    void UpdateBalken()
    {
        float scale = (float)defender.GetHealth() / startHealth;
        balken.transform.localScale = new Vector3(balken.transform.localScale.x, balken.transform.localScale.y, scale * startSize);
        print("nachher" + balken.transform.localScale);
    }

    void RotateHealthBar()
    {
        transform.LookAt(door.transform.position + new Vector3(0f, 20f, 0f));
    }
}
