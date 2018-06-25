using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zawiasy2 : MonoBehaviour {

    //1
    private SteamVR_TrackedObject trackedObj;

    //2
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    // 1
    private GameObject collidingObject;
    // 2 
    //private GameObject objectInHand;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    private void SetCollidingObject(Collider col)
    {

        // 1
        if (collidingObject || !col.GetComponent<Zawiasy>())
        {
            return;
        }

        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;

    }


    void Update()
    {

        if (Controller.GetHairTrigger())
        {
            if (collidingObject)
            {
                Vector3 v = transform.position;
                v.y = collidingObject.transform.position.y;

                collidingObject.transform.LookAt(v);
                collidingObject.transform.Rotate(collidingObject.GetComponent<Zawiasy>().correction);
            }
        }


    }
}
