using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAggroTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attacker"))
        {
            Defender defender = transform.parent.GetComponent<Defender>();
            defender.AddAttackerInRange(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Attacker"))
        {
            Defender defender = transform.parent.GetComponent<Defender>();
            defender.RemoveAttackerInRange(other.gameObject);
        }
    }
}
