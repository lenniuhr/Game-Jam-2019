using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class EventScript : MonoBehaviour {

    public SteamVR_TrackedObject mTrackedObjekt = null;
    public SteamVR_Controller.Device mDevice;
    public BuildingSelectionScript BuildingSkript;
    public SpellCasting SpellCastingScript;

    void Awake()
    {
        mTrackedObjekt = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        mDevice = SteamVR_Controller.Input((int)mTrackedObjekt.index);
        // 1
        if (mDevice.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + mDevice.GetAxis());
        }

        // 2
        if (mDevice.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + " Trigger Press");
                BuildingSkript.Trigger();
                SpellCastingScript.Trigger();
        }

        // 3
        if (mDevice.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + " Trigger Release");
                BuildingSkript.TriggerExit();        
                StartCoroutine(SpellCastingScript.TriggerExit());


        }

        // 4
        if (mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Press");
        }

        // 5
        if (mDevice.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Release");
        }
    }

}
