using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomAtFlystick : MonoBehaviour
{

    public AudioSource doorOpenSound;


    // 1
    private GameObject collidingObject;
    // 2 
    private GameObject objectInHand;

    public int flystickIdx = 0;

    private void SetCollidingObject(Collider col)
    {

        // 1
        if (collidingObject)
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


    bool previousActiveBtn = false;

    // Update is called once per frame
    void Update()
    {
        if (!LZWPlib.Core.Instance.isServer)
            return;

        bool activeBtn = LZWPlib.LzwpTracking.Instance.flysticks[flystickIdx].fire.isActive;

        if (activeBtn && !previousActiveBtn)
        {
            if (collidingObject)
            {
                Debug.Log("KOL");
                IndexKlamki index_klamki = collidingObject.GetComponent<IndexKlamki>();
                if (index_klamki != null)
                {

                    Debug.Log("idx klamki " + index_klamki.indexKlamki);
                    RoomChanger.Instance.jakas_metoda(index_klamki.indexKlamki);
                    doorOpenSound.Play();
                }

            }
        }

        previousActiveBtn = activeBtn;
    }

}
