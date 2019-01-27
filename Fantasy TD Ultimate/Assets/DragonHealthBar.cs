using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHealthBar : MonoBehaviour {

    GameObject door;
    Dragon dragon;
    Transform balken;
    float startSize;
    int startHealth;

    // Use this for initialization
    void Start()
    {
        door = GameObject.Find("Door");
        dragon = GetComponentInParent<Dragon>();
        balken = transform.Find("Balken");
        startSize = balken.transform.localScale.z;
        startHealth = dragon.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBalken();
        RotateHealthBar();
    }

    void UpdateBalken()
    {
        float scale = (float)dragon.GetHealth() / startHealth;
        balken.transform.localScale = new Vector3(balken.transform.localScale.x, balken.transform.localScale.y, scale * startSize);


    }

    void RotateHealthBar()
    {
        transform.LookAt(door.transform.position + new Vector3(0f, 20f, 0f));
    }
}
