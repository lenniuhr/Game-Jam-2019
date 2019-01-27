using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attacker : BattleObject {

    public int attackDamage;
    public float moveSpeed;
    public float attackDistance;
    public float attackOffset;
    public float attackCooldown;

    private GameObject door;
    private List<GameObject> defendersInRange = new List<GameObject>();

    protected float timeSinceLastAttack;
    protected GameObject target;

    // Use this for initialization
    protected void Start () {
        door = GameObject.Find("Door");
        target = door;
        print(target);
    }
	
	// Update is called once per frame
    protected void Update ()
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

    protected abstract void MoveInDirection(Transform targetTransform);

    private void CheckNextAttacker()
    {
        if(door == null) // game over
        {
            return;
        }

        if(target == null || target == door || target.GetComponent<BattleObject>().HasDied())
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
}
