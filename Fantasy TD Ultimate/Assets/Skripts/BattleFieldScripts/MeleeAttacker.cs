using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : Attacker {

    private Animator animator;

    // Use this for initialization
    protected void Start () {
        base.Start();
        animator = GetComponentInChildren<Animator>();
    }

    protected override void MoveInDirection(Transform targetTransform)
    {
        transform.LookAt(targetTransform.position);

        Vector3 direction = transform.position - targetTransform.position;
        if (direction.magnitude > attackDistance)
        {
            transform.Translate(new Vector3(0f, 0f, moveSpeed) * Time.deltaTime);
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
        if (timeSinceLastAttack > attackCooldown)
        {
            print("HIT");
            target.GetComponent<BattleObject>().TakeDamage(attackDamage);
            timeSinceLastAttack = 0f;
        }
    }

    protected override void HandleDeath()
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
