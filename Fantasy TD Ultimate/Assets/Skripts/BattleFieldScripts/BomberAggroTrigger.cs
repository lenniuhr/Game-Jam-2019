using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAggroTrigger : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Defender"))
        {
            Bomberwagen attacker = transform.parent.GetComponent<Bomberwagen>();
            attacker.AddDefenderInRange(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Defender"))
        {
            Bomberwagen attacker = transform.parent.GetComponent<Bomberwagen>();
            attacker.RemoveDefenderInRange(other.gameObject);
        }
    }
}
