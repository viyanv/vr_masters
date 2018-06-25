using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomAtController : MonoBehaviour
{

    //1
    private SteamVR_TrackedObject trackedObj;
    public AudioSource doorOpenSound;

    //2
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    // 1
    private GameObject collidingObject;
    // 2 
    private GameObject objectInHand;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

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

    // Update is called once per frame
    void Update()
    {

        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                Debug.Log("KOL");
                IndexKlamki index_klamki = collidingObject.GetComponent<IndexKlamki>();
                if(index_klamki != null)
                {

                    Debug.Log("idx klamki " + index_klamki.indexKlamki);
                    RoomChanger.Instance.jakas_metoda(index_klamki.indexKlamki);
                    doorOpenSound.Play();
                }
                
            }
        }

    }

}
