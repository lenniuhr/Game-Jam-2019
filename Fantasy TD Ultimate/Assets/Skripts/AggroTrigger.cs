using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroTrigger : MonoBehaviour {

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
            Attacker attacker = transform.parent.GetComponent<Attacker>();
            attacker.AddDefenderInRange(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Defender"))
        {
            Attacker attacker = transform.parent.GetComponent<Attacker>();
            attacker.RemoveDefenderInRange(other.gameObject);
        }
    }
}
