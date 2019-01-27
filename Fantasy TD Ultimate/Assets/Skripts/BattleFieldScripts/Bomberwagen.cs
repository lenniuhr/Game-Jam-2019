using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomberwagen : MonoBehaviour {

    public int health = 100;
    public int attackDamage = 10;
    public float moveSpeed;
    public float attackRange = 10f;
    public float attackRadius = 15f;
    public GameObject explosion;
    private bool isExploded;

    private GameObject door;
    private GameObject target;
    private List<GameObject> defendersInRange = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        door = GameObject.Find("Door");
        target = door;
    }

    // Update is called once per frame
    void Update()
    {
        CheckNextAttacker();

        if (!HasDied())
        {
            if (target != null)
            {
                MoveInDirection(target.transform);
            }
        }
    }

    private void MoveInDirection(Transform targetTransform)
    {
        transform.LookAt(targetTransform.position);

        Vector3 direction = transform.position - targetTransform.position;
        if (direction.magnitude > attackRange)
        {
            transform.Translate(new Vector3(0f, 0f, moveSpeed));
        }
        else
        {
            Explode();
        }
    }

    private void Explode()
    {
        if(!isExploded)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Invoke("DealDamage", 3.5f);
            isExploded = true;
            Destroy(gameObject, 3.6f);
        }

    }
    void DealDamage()
    {
        print("DealDamage called" + target);
        List<Defender> damageTakers = new List<Defender>();
        defendersInRange.Add(target);
        foreach(GameObject defender in defendersInRange)
        {
            print((transform.position - defender.transform.position).magnitude);
            if((transform.position - defender.transform.position).magnitude < attackRadius)
            {
                damageTakers.Add(defender.GetComponent<Defender>());
            }
        }

        foreach(Defender defender in damageTakers)
        {
            defender.TakeDamage(attackDamage);
        }
    }

    private void CheckNextAttacker()
    {
        if (door == null) // game over
        {
            return;
        }

        if (target == null || target == door || target.GetComponent<Defender>().HasDied())
        {
            if (defendersInRange.Count > 0)
            {
                target = defendersInRange[0];
                defendersInRange.Remove(target);
            }
            else
            {
                target = door;
            }
        }
    }

    public void AddDefenderInRange(GameObject o)
    {
        defendersInRange.Add(o);
        CheckNextAttacker();
    }

    public void RemoveDefenderInRange(GameObject o)
    {
        if (target == o)
        {
            target = null;
        }
        else
        {
            defendersInRange.Remove(o);
        }
        CheckNextAttacker();
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Explode();
        }
    }

    public bool HasDied()
    {
        return health <= 0;
    }

    public int GetHealth()
    {
        return health;
    }
}
