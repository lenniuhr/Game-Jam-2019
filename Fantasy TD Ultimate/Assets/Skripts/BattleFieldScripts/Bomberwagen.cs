using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomberwagen : Attacker {

    public float attackRange = 10f;
    public float attackRadius = 15f;
    public GameObject explosion;
    private bool isExploded;

    private List<GameObject> defendersInRange = new List<GameObject>();

    protected override void HandleDeath()
    {
        Explode();
    }

    protected override void MoveInDirection(Transform targetTransform)
    {
        transform.LookAt(targetTransform.position);

        Vector3 direction = transform.position - targetTransform.position;
        if (direction.magnitude > attackRange)
        {
            transform.Translate(new Vector3(0f, 0f, moveSpeed) * Time.deltaTime);
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
            GameObject deathAnim = Instantiate(explosion, transform.position, Quaternion.identity);
            deathAnim.transform.Rotate(new Vector3(-90, 0, 0));
            Invoke("DealDamage", 3.5f);
            isExploded = true;
            Destroy(gameObject, 3.6f);
        }

    }
    void DealDamage()
    {
        print("DealDamage called" + target);
        List<BattleObject> damageTakers = new List<BattleObject>();
        defendersInRange.Add(target);
        foreach(GameObject defender in defendersInRange)
        {
            print((transform.position - defender.transform.position).magnitude);
            if((transform.position - defender.transform.position).magnitude < attackRadius)
            {
                damageTakers.Add(defender.GetComponent<BattleObject>());
            }
        }

        foreach(BattleObject defender in damageTakers)
        {
            defender.TakeDamage(attackDamage);
        }
    }
}
