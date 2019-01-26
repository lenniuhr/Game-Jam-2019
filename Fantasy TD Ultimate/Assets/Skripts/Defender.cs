using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour {

    private List<GameObject> attackersInRange;
    private float timeSinceLastAttack = 10f;
    private GameObject currentTarget;
    private GameObject projectiles;

    public GameObject missile;
    public float shotPerSecond = 0.1f;

    // Use this for initialization
    void Start () {
        attackersInRange = new List<GameObject>();
        projectiles = GameObject.Find("Projectiles");
	}
	
	// Update is called once per frame
	void Update () {
        CheckNextTarget();

        if (timeSinceLastAttack > 1f/shotPerSecond && currentTarget != null)
        {
            shoot();
        }
        else
        {
            timeSinceLastAttack += Time.deltaTime;
        }
    }

    private void shoot()
    {
        GameObject newMissile = Instantiate(missile, projectiles.transform);
        newMissile.GetComponent<Missile>().SetTarget(currentTarget);
        newMissile.transform.Translate(transform.position);
        timeSinceLastAttack = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Attacker"))
        {
            GameObject target = other.gameObject;
            attackersInRange.Add(target);
            CheckNextTarget();
        }

    }

    private void CheckNextTarget()
    {
        if (currentTarget == null && attackersInRange.Count > 0)
        {
            currentTarget = attackersInRange[0];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Attacker"))
        {
            GameObject attacker = other.gameObject;
            attackersInRange.Remove(attacker);
        }
    }
}
