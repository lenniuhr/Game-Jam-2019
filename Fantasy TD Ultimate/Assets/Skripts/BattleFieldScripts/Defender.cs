using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour {

    public int maxHealth = 100;
    private int currentHealth;
    public int damage = 50;

    private List<GameObject> attackersInRange;
    private float timeSinceLastAttack = 10f;
    private GameObject target;
    private GameObject projectiles;

    public GameObject missile;
    public float shotPerSecond = 0.1f;

    // Use this for initialization
    void Start () {
        attackersInRange = new List<GameObject>();
        projectiles = GameObject.Find("Projectiles");
        currentHealth = maxHealth;
	}

    // Update is called once per frame
    void Update () {
        CheckNextTarget();

        if (timeSinceLastAttack > 1f/shotPerSecond && target != null)
        {
            Shoot();
        }
        else
        {
            timeSinceLastAttack += Time.deltaTime;
        }
    }

    private void Shoot()
    {
        GameObject newMissile = Instantiate(missile, projectiles.transform);
        newMissile.GetComponent<Missile>().SetTargetAndDamage(target, damage);
        newMissile.transform.Translate(transform.position);
        timeSinceLastAttack = 0f;
    }

    private void CheckNextTarget()
    {
        if (target != null && target.GetComponent<Attacker>().HasDied()) // target has died
        {
            target = null;
        }

        if (target == null && attackersInRange.Count > 0) // no target
        {
            target = attackersInRange[0];
            attackersInRange.Remove(target);
        }
    }

    internal void AddAttackerInRange(GameObject o)
    {
        attackersInRange.Add(o);
        CheckNextTarget();
    }

    internal void RemoveAttackerInRange(GameObject o)
    {
        if (target == o)
        {
            target = null;
        }
        else
        {
            attackersInRange.Remove(o);
        }
        CheckNextTarget();
    }

    internal void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }

    public bool HasDied()
    {
        return currentHealth <= 0;
    }
}
