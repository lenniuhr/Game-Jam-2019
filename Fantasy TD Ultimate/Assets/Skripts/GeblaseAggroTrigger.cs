using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeblaseAggroTrigger : MonoBehaviour {

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
            Geblase defender = transform.parent.GetComponent<Geblase>();
            defender.AddAttackerInRange(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Attacker"))
        {
            Geblase defender = transform.parent.GetComponent<Geblase>();
            defender.RemoveAttackerInRange(other.gameObject);
        }
    }
}
