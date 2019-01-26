using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectionScript : MonoBehaviour {

    public GameObject[] BuildEffect;
    private GameObject SelectedBuilding;
    private GameObject HoldingBuilding;
    private int BuildIndex = 0;

    private bool trigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Build"))
        {
            if (other.gameObject.tag.Contains("1"))
            {
                BuildIndex = 0;
            } else if (other.gameObject.tag.Contains("2"))
            {
                BuildIndex = 1;
            }
            other.GetComponent<BuildGetsCollected>().GotSelected();
            SelectedBuilding = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (SelectedBuilding)
        {
            other.GetComponent<BuildGetsCollected>().GotDeSelected();
            SelectedBuilding = null;
        }
    }

    public void Trigger()
    {
        Debug.Log("MyTrigger");
        if (SelectedBuilding)
        {
            Debug.Log("BuildingSelected");
            trigger = true;
            HoldingBuilding = Instantiate(BuildEffect[BuildIndex]) as GameObject;

            HoldingBuilding.transform.parent = this.transform;
            //HoldingBuilding.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
            // HoldingBuilding.GetComponent<Transform>().position = new Vector3(HoldingBuilding.GetComponent<Transform>().position.x, HoldingBuilding.GetComponent<Transform>().position.y + 0.3f, HoldingBuilding.GetComponent<Transform>().position.z);
            //HoldingBuilding.GetComponent<Transform>().rotation = this.GetComponentInParent<Transform>().rotation;
            HoldingBuilding.GetComponent<Transform>().localPosition = new Vector3(0f, 0.3f, 0f);
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
