using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour {

    public GameObject[] Spell;
    public GameObject Ray;
    public int RapidFire = 10;
    private GameObject SpellInstance;
    private int SpellIndex = 0;
    private GameObject SelectedSpell;
    private GameObject HoldingSpell;
    private bool trigger = false;
    private Vector3 Aim;


    // Wenn auf ein Spell gezeigt wird gebe Feedback und speicher das entsprechende Gebäude
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag.Contains("Spell") && SelectedSpell == null) && (trigger == false) )
        {
            if (other.gameObject.tag.Contains("1"))
            {
                SpellIndex = 0;
            }
            else if (other.gameObject.tag.Contains("2"))
            {
                SpellIndex = 1;
            }
            other.GetComponent<SpellGetsCollected>().GotSelected();
            SelectedSpell = other.gameObject;
            Debug.Log(SpellIndex);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (SelectedSpell)
        {
            other.GetComponent<SpellGetsCollected>().GotDeSelected();
            SelectedSpell = null;
        }
    }

    public void Trigger()
    {
        Debug.Log("MyTrigger");
        if (SelectedSpell)
        {
            Debug.Log("BuildingSelected");
            trigger = true;
            HoldingSpell = Instantiate(Spell[SpellIndex]) as GameObject;

            HoldingSpell.transform.parent = this.transform;
            //HoldingBuilding.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
            // HoldingBuilding.GetComponent<Transform>().position = new Vector3(HoldingBuilding.GetComponent<Transform>().position.x, HoldingBuilding.GetComponent<Transform>().position.y + 0.3f, HoldingBuilding.GetComponent<Transform>().position.z);
            HoldingSpell.GetComponent<Transform>().rotation = this.GetComponentInParent<Transform>().rotation;
            HoldingSpell.GetComponent<Transform>().localPosition = new Vector3(0f, 0.3f, 0f);

            //Activiere den Raycast
            Ray.SetActive(true);
        }

    }

    public IEnumerator TriggerExit()
    {
        Debug.Log(SpellIndex);
        if (SpellIndex == 0)
        {
            GameObject FireSpell = Instantiate(HoldingSpell);
            FireSpell.GetComponent<Transform>().localPosition = HoldingSpell.GetComponent<Transform>().position;
            FireSpell.GetComponent<SpellFire>().Fire(Aim);
            yield return new WaitForSeconds(.01f); ;
        }
        else if (SpellIndex == 1)
        {
            int LocalCounter = RapidFire;

            while (LocalCounter > 0)
            {
                GameObject FireSpell = Instantiate(HoldingSpell);
                FireSpell.GetComponent<Transform>().localPosition = HoldingSpell.GetComponent<Transform>().position;
                FireSpell.GetComponent<SpellFire>().Fire(Aim);
                LocalCounter--;
                HoldingSpell.SetActive(false);
                yield return new WaitForSeconds(.2f);
                HoldingSpell.SetActive(true);
                yield return new WaitForSeconds(.2f);
            }

        }
        Destroy(HoldingSpell);
        Ray.SetActive(false);
        trigger = false;
    }



    public void CastSpell(Vector3 position, Vector3 direktion)
    {
        SpellInstance = Instantiate(Spell[SpellIndex]);
        SpellInstance.GetComponent<Transform>().position = position;
        SpellInstance.GetComponent<Transform>().rotation = this.GetComponentInParent<Transform>().rotation;
    }

    public void Update()
    {
        if (trigger)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(0f, 1f, 0f) * 200, Color.yellow);
            Aim = transform.TransformDirection(0f, 1f, 0f) * 400;


        }
    }

}
