using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkript : MonoBehaviour {

    public int Mana = 100;
    public int Credits = 100 ;
    public GameObject ManaBar;
    public float ManaRegeneration = 100f;
    
        private float timer = 0;
    public bool WaveMode = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        ManaBar.getComponent<Transform>.scale = new Vector3(Mana / 100f, 1, 1);
        if (Mana < 100)
        {
            UpdateMana();
        }
	}

    private void UpdateMana()
    {
        if (timer < ManaRegeneration)
        {
            timer += 1;
        }
        else
        {
            Mana += 1;
        }
    }
}
