using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {

    private int currentHealth;
    public int maxHealth = 100;

    public int attackDamage = 10;

    public float attackCooldown = 2f;
    private float timeSinceLastAttack;

    public float moveSpeed;
    public float attackDistance;

    public bool isWalking = true;
    public bool isAttacking;
    private bool IsDying;

    private GameObject door;
    private Animator animator;

    List<GameObject> defendersInRange = new List<GameObject>();
    private GameObject target;

    // Use this for initialization
    void Start () {
        door = GameObject.Find("Door");
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckNextAttacker();

        if (target != null)
        {
            MoveInDirection(target.transform);
        }
        else
        {
            MoveInDirection(door.transform);
        }
    }

    private void MoveInDirection(Transform targetTransform)
    {
        transform.LookAt(targetTransform.position);

        Vector3 direction = transform.position - targetTransform.position;
        if(direction.magnitude > attackDistance)
        {transform.Translate(new Vector3(0f, 0f, moveSpeed));
            
            animator.SetTrigger("WalkTrigger");
        }
        else
        {
            animator.SetTrigger("AttackTrigger");
            Attack();
        }
    }

    private void Attack()
    {
        if(timeSinceLastAttack > attackCooldown)
        {
            print(target);
            target.GetComponent<Defender>().TakeDamage(attackDamage);
        }
        timeSinceLastAttack += Time.deltaTime;
    }

    private void CheckNextAttacker()
    {
        if (target != null && target.GetComponent<Defender>().HasDied()) // target has died
        {
            target = null;
        }

        if (target == null && defendersInRange.Count > 0)
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
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.SetTrigger("DieTrigger");
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(gameObject, 4);
        }
    }

    public bool HasDied()
    {
        return currentHealth <= 0;
    }
}
