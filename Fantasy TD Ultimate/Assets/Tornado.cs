using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour {

    public float missileSpeed = 1f;
    private Vector3 direction;
    private int damage;
    private float maxRange = 50f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(maxRange < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(direction.normalized * missileSpeed);
            maxRange -= (direction.normalized * missileSpeed).magnitude;
        }
    }

    public void SetTargetAndDamage(Vector3 direction, int damage)
    {
        this.direction = direction;
        this.damage = damage;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Attacker"))
        {
            collider.gameObject.GetComponent<BattleObject>().TakeDamage(damage);
        }
    }
}
