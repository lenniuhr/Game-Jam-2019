using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

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
            attacker.AddDefenderInHitRange(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Defender"))
        {
            Dragon attacker = transform.parent.GetComponent<Dragon>();
            attacker.RemoveDefenderInHitRange(other.gameObject);
        }
    }
}
