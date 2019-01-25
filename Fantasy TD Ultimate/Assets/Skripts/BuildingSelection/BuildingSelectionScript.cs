using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectionScript : MonoBehaviour {



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Build"))
        {
            other.GetComponent<BuildGetsCollected>().GotSelected();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<BuildGetsCollected>().GotDeSelected();
    }
}
