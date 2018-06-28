using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZawiasyCave : MonoBehaviour
{

    // 1
    private GameObject collidingObject;
    // 2 
    //private GameObject objectInHand;



    public int flystickIdx = 0;


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
        if (!LZWPlib.Core.Instance.isServer)
            return;

        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        if (!LZWPlib.Core.Instance.isServer)
            return;

        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!LZWPlib.Core.Instance.isServer)
            return;

        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;

    }


    void Update()
    {
        if (!LZWPlib.Core.Instance.isServer)
            return;

        if (LZWPlib.LzwpTracking.Instance.flysticks[flystickIdx].fire.isActive)
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
