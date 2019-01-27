using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectionScript : MonoBehaviour {

    public GameObject[] BuildEffect;
    public GameObject[] BuildPlacement;
    public GameObject Ray;
    public LevelManager LevelManagerScript;
    public int BuildCap = 1;
    private GameObject SelectedBuilding = null;
    private GameObject HoldingBuilding;
    private GameObject BuildPlacementInstance;
    private int BuildIndex = 0;
    private RaycastHit hit;

    private bool trigger = false;
    private bool BuildTrigger = false;

    // Wenn auf ein Gebäude oder Spell gezeigt wird gebe Feedback und speicher das entsprechende Gebäude
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Build") && SelectedBuilding == null)
        {
            if (other.gameObject.tag.Contains("1"))
            {
                BuildIndex = 0;
            } else if (other.gameObject.tag.Contains("2"))
            {
                BuildIndex = 1;
            }
            else if (other.gameObject.tag.Contains("3"))
            {
                BuildIndex = 2;
            }

            other.GetComponent<BuildGetsCollected>().GotSelected();
            SelectedBuilding = other.gameObject; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (SelectedBuilding)
        {
            if (other.gameObject.tag.Contains("Build"))
            {
                other.GetComponent<BuildGetsCollected>().GotDeSelected();
                SelectedBuilding = null;
            }
            
        }
    }

    //Sobald der Triggerbutton gedrückt wird
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

            //Erstelle das DummyGebäude
            BuildPlacementInstance = Instantiate(BuildPlacement[BuildIndex]);
            BuildPlacementInstance.SetActive(false);

            //Activiere den Raycast
            Ray.SetActive(true);
        }

    }

    public void TriggerExit()
    {
        if (HoldingBuilding)
        {
            trigger = false;
            Ray.SetActive(false);
            if (BuildTrigger)
            {
                Instantiate(BuildPlacementInstance);
                BuildCap--;
                if (BuildCap <= 0)
                {
                    LevelManagerScript.BuildPhase = false;
                    LevelManagerScript.StartWave();
                }
            }
            BuildPlacementInstance.SetActive(false);
            Destroy(HoldingBuilding);
        }

    }

    public void Update()
    {
        if (trigger) {
            Debug.DrawRay(transform.position, transform.TransformDirection(0f, 1f, 0f) * 100, Color.yellow);
            if (Physics.Raycast(transform.position, transform.TransformDirection(0f, 1f, 0f), out hit, Mathf.Infinity, 1 << 12))
            {
                BuildPlacementInstance.GetComponent<Transform>().position = hit.point;
                BuildPlacementInstance.SetActive(true);
                BuildTrigger = true;
            }
            else
            {
                BuildTrigger = false;
                BuildPlacementInstance.SetActive(false);
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                //Debug.Log("Did not Hit");
            }


        }
    }
}
