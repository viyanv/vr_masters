using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerLoadLevel : MonoBehaviour
{
    [SerializeField]
    private int loadLevel;
    /*
    private SteamVR_TrackedObject trackedObjL;
    private SteamVR_TrackedObject trackedObjR;

    private SteamVR_Controller.Device ControllerL
    {
        get { return SteamVR_Controller.Input((int)trackedObjL.index); }
    }

    private SteamVR_Controller.Device ControllerR
    {
        get { return SteamVR_Controller.Input((int)trackedObjR.index); }
    }

    void Start()
    {
        trackedObjL = GameObject.Find("Controller (left)").GetComponent<SteamVR_TrackedObject>();
        trackedObjR = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedObject>();
    }

    bool inColl = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            inColl = true;
            //SceneManager.LoadScene(loadLevel);
            //kuchnia.SetActive(loadLevel == "kuchnia");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inColl = false;
        }
    }

    void Update()
    {
        if (inColl && (ControllerL.GetHairTriggerDown() || ControllerR.GetHairTriggerDown()))
        {

            RoomChanger.Instance.jakas_metoda(loadLevel);
            //SceneManager.LoadScene(loadLevel);
            //kuchnia.SetActive(loadLevel == "kuchnia");
        }
    }
        */

}
