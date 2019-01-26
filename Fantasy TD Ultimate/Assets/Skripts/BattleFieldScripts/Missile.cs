using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public float missileSpeed = 1f;
    public int damage = 10;

    private GameObject myTarget;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update ()
    {
        if (myTarget != null)
        {
            Vector3 direction = transform.position - myTarget.transform.position;
            Vector3 directionNorm = direction.normalized;
            if(direction.magnitude < 0)
            {
                transform.Translate(directionNorm * missileSpeed);
            }
            else
            {
                transform.Translate(-directionNorm * missileSpeed);
            }
        }
    }

    public void SetTargetAndDamage(GameObject target, int damage)
    {
        this.myTarget = target;
        this.damage = damage;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Attacker"))
        {
            collider.gameObject.GetComponent<Attacker>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
