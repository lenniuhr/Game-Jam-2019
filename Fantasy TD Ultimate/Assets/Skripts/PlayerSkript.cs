using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkript : MonoBehaviour {

    public float Mana = 100f;
    public float Credits = 100f;
    public GameObject ManaBar;
    public GameObject[] ManaBars;
    public Material[] Mats;
    public int lastSpell = 5;
    public SpellCasting SpellCastingScript;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Mana < 100)
        {
            Mana += Time.deltaTime * lastSpell;
            if (Mana >= 100)
            {
                ManaBars[0].GetComponent<Renderer>().material = Mats[1];
                ManaBars[1].GetComponent<Renderer>().material = Mats[1];
                SpellCastingScript.SpellReady = true;
            }
        }
        ManaBar.GetComponent<Transform>().localScale = new Vector3(Mana / 100f, 1f, 1f);

    }

    public void SpellSelected(int index){
        if (index == 0)
        {
            lastSpell = 30;
        }
        if (index == 1)
        {
            lastSpell = 20;
        }
        if (index == 2)
        {
            lastSpell = 20;
        }
        Mana = 1;
        ManaBars[0].GetComponent<Renderer>().material = Mats[0];
        ManaBars[1].GetComponent<Renderer>().material = Mats[0];

    }
}
