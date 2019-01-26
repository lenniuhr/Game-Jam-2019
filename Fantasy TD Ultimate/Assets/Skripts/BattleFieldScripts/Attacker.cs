using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {


    public int maxHealth = 100;

    public int attackDamage = 10;

    public float attackCooldown = 2f;
    public float attackOffset;
    private float timeSinceLastAttack;

    public float moveSpeed;
    public float attackDistance;

    private GameObject door;
    private Animator animator;

    List<GameObject> defendersInRange = new List<GameObject>();
    private GameObject target;

    // Use this for initialization
    void Start () {
        door = GameObject.Find("Door");
        target = door;
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckNextAttacker();

        if(!HasDied())
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
        if(direction.magnitude > attackDistance)
        {
            transform.Translate(new Vector3(0f, 0f, moveSpeed));
            animator.SetTrigger("WalkTrigger");
            timeSinceLastAttack = attackCooldown - attackOffset;
        }
        else
        {
            print(direction.magnitude);
            animator.SetTrigger("AttackTrigger");
            Attack();
            timeSinceLastAttack += Time.deltaTime;
        }
    }

    private void Attack()
    {
        if(timeSinceLastAttack > attackCooldown)
        {
            print("HIT");
            target.GetComponent<Defender>().TakeDamage(attackDamage);
            timeSinceLastAttack = 0f;
        }
    }

    private void CheckNextAttacker()
    {
        if(door == null)
        {
            return;
        }

        if(target == null)
        {
            target = door;
        }

        if (target == door && defendersInRange.Count > 0)
        {
            target = defendersInRange[0];
            defendersInRange.Remove(target);
        }
    }

    public void AddDefenderInRange(GameObject o)
    {
        defendersInRange.Add(o);
        CheckNextAttacker();
    }

    public void RemoveDefenderInRange(GameObject o)
    {
        if(target == o)
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
        maxHealth -= dmg;

        if (maxHealth <= 0)
        {
            maxHealth = 0;
            animator.SetTrigger("DieTrigger");
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(gameObject, 2);
        }
    }

    public bool HasDied()
    {
        return maxHealth <= 0;
    }
}
