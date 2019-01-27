using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : BattleObject
{
    public float moveSpeed;
    public int damagePerFrame;
    public float attackDistance;

    private Animator animator;
    private ParticleSystem fireStream;
    private List<GameObject> defendersInHitRange = new List<GameObject>();
    protected GameObject target;
    private GameObject door;
    private List<GameObject> defendersInRange = new List<GameObject>();

    // Use this for initialization
    protected void Start()
    {
        door = GameObject.Find("Door");
        target = door;
        animator = GetComponentInChildren<Animator>();
        fireStream = GetComponentInChildren<ParticleSystem>(true);
    }

    private void FixedUpdate()
    {
        CheckNextAttacker();

        if (!HasDied())
        {
            if (target != null)
            {
                transform.LookAt(target.transform.position);

                Vector3 direction = transform.position - target.transform.position;
                if (direction.magnitude > attackDistance)
                {
                    transform.Translate(new Vector3(0f, 0f, moveSpeed) * Time.deltaTime);
                    animator.SetTrigger("WalkTrigger");
                    fireStream.gameObject.SetActive(false);
                }
                else
                {
                    Attack();
                }
            }
        }
    }

    private void CheckNextAttacker()
    {
        if (door == null) // game over
        {
            return;
        }

        if (target == null || target == door || target.GetComponent<BattleObject>().HasDied())
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

    private void Attack()
    {
        animator.SetTrigger("AttackTrigger");
        fireStream.gameObject.SetActive(true);
        foreach(GameObject o in defendersInHitRange)
        {
            if(o != null)
            {
                BattleObject battle = o.GetComponent<BattleObject>();
                battle.TakeDamage(damagePerFrame);
                if (battle.HasDied())
                {
                    defendersInRange.Remove(o);
                }
            }
        }
    }

    protected override void HandleDeath()
    {
        health = 0;
        animator.SetTrigger("DieTrigger");
        fireStream.gameObject.SetActive(false);
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(gameObject, 6);
    }

    public void AddDefenderInHitRange(GameObject gameObject)
    {
        defendersInHitRange.Add(gameObject);
    }

    public void RemoveDefenderInHitRange(GameObject gameObject)
    {
        defendersInHitRange.Remove(gameObject);
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
}
