using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFire : MonoBehaviour {

    public float speed = 1f;
    public int damage = 100;
    private Vector3 MyTarget;
    private bool Firing = false;
    private Vector3 OldPosition = new Vector3 (0, 0, 0);

    public void Fire(Vector3 Target)
    {
        MyTarget = Target;
        Firing = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Firing) {
            this.transform.position = Vector3.MoveTowards(this.transform.position, MyTarget, speed);
            if (this.transform.position == OldPosition)
            {
                Destroy(this.gameObject);
            }
            OldPosition = this.transform.position;            
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Attacker"))
        {
            Debug.Log("EnemyHit");
            collider.gameObject.GetComponent<Attacker>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
