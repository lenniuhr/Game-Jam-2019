using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectionScript : MonoBehaviour {

    private GameObject SelectedBuilding;
    private GameObject HoldingBuilding;

    private bool trigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Build"))
        {
            other.GetComponent<BuildGetsCollected>().GotSelected();
            SelectedBuilding = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<BuildGetsCollected>().GotDeSelected();
        SelectedBuilding = null;
    }

    public void Trigger()
    {
        Debug.Log("MyTrigger");
        if (SelectedBuilding)
        {
            Debug.Log("BuildingSelected");
            trigger = true;
            HoldingBuilding = Instantiate(SelectedBuilding);
           // HoldingBuilding.transform.parent = this.transform;
           // HoldingBuilding.transform.position = new Vector3(0, 0, 0);
        }
         
    }

    public void TriggerExit()
    {
        if (HoldingBuilding)
        {
            trigger = false;
            Destroy(HoldingBuilding);
        }

    }

    public void Update()
    {
        if (trigger) {
           
                }
    }
}
