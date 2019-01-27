using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {


    public int health = 100;

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
            animator.SetTrigger("AttackTrigger");
            Attack();
            timeSinceLastAttack += Time.deltaTime;
        }
    }

    private void Attack()
    {
        if(timeSinceLastAttack > attackCooldown)
        {
            target.GetComponent<Defender>().TakeDamage(attackDamage);
            timeSinceLastAttack = 0f;
        }
    }

    private void CheckNextAttacker()
    {
        if(door == null) // game over
        {
            return;
        }

        if(target == null || target == door || target.GetComponent<Defender>().HasDied())
        {
            if(defendersInRange.Count > 0)
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
        health -= dmg;

        if (health <= 0)
        {
            health = 0;
            animator.SetTrigger("DieTrigger");
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(gameObject, 2);

            if (animator != null)
            {
                animator.SetTrigger("DieTrigger");
            }
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
