using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

    //1
    private SteamVR_TrackedObject trackedObj;
    public AudioSource orbSound;
    public AudioSource kettleSoundEnd;
    public AudioSource kettleSoundAll;
    public bool alreadyClicked = false;

    public float fadeTime = 1; // fade time in seconds
 public void FadeSound() { 
     if(fadeTime == 0) { 
         kettleSoundAll.volume = 0;
         return;
     }
     StartCoroutine(_FadeSound()); 
 }
 
 IEnumerator _FadeSound() {
     float t = fadeTime;
     while (t > 0) {
         yield return null;
         t-= Time.deltaTime/2;
         kettleSoundAll.volume = t/fadeTime;
     }
     yield break;
 }

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
        if (collidingObject || !col.GetComponent<Rigidbody>())
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

    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        if (objectInHand.CompareTag("kettle"))
        {
            if (!alreadyClicked)
            {
                FadeSound();
                alreadyClicked = true;
            }
            
        }

    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        if (objectInHand.CompareTag("orb"))
        {
            Timer.score += 1;
            orbSound.Play();
            Destroy(objectInHand);
        }
        if (objectInHand.CompareTag("orb2kettle"))
        {
            Timer.score += 1;
            orbSound.Play();
            kettleSoundAll.Play();
            Destroy(objectInHand);
        }
        objectInHand = null;
    }

	// Update is called once per frame
	void Update () {

        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
		
	}

    void FixedUpdate()
    {
        if (objectInHand != null)
        {
            objectInHand.GetComponent<Rigidbody>().velocity = Vector3.zero;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        
    }
}
