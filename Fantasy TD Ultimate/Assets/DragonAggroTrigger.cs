using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAggroTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Defender"))
        {
            Dragon attacker = transform.parent.GetComponent<Dragon>();
            attacker.AddDefenderInRange(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Defender"))
        {
            Dragon attacker = transform.parent.GetComponent<Dragon>();
            attacker.RemoveDefenderInRange(other.gameObject);
        }
    }
}
