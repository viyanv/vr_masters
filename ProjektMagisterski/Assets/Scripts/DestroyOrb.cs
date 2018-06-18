using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOrb : MonoBehaviour {

    public GameObject OrbItem;
    public bool clickedButton = false;

    //1
    private SteamVR_TrackedObject trackedObj;

    //2
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (clickedButton)
                Destroy(OrbItem);
        }
    }

	// Update is called once per frame
	void Update () {
        if (Controller.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + " Trigger Press");
            clickedButton = true;
        }
	}
}
