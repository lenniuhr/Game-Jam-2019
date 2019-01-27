using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGetsCollected : MonoBehaviour {

    public GameObject Tower;
    public float SelectionScale = 0.2f; 

	// Use this for initialization
    public void GotSelected()
    {
        Tower.SetActive(true);
        /*
        this.transform.localScale = new Vector3(this.transform.localScale.x + SelectionScale, this.transform.localScale.y + SelectionScale, this.transform.localScale.z + SelectionScale);
        this.GetComponent<Renderer>().material = Mat[1];
        */
    }

    public void GotDeSelected()
    {
        Tower.SetActive(false);
        /*
        this.transform.localScale = new Vector3(this.transform.localScale.x - SelectionScale, this.transform.localScale.y - SelectionScale, this.transform.localScale.z - SelectionScale);
        this.GetComponent<Renderer>().material = Mat[0];
        */
    }
}
