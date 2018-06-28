using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlystickGrabObject : MonoBehaviour
{

    //1
    public AudioSource orbSound;
    public AudioSource kettleSoundEnd;
    public AudioSource kettleSoundAll;
    public static bool alreadyClicked = false;

    public float fadeTime = 1; // fade time in seconds

    public int flystickIdx = 0;

    public void FadeSound()
    {
        if (fadeTime == 0)
        {
            kettleSoundAll.volume = 0;
            return;
        }
        StartCoroutine(_FadeSound());
    }

    IEnumerator _FadeSound()
    {
        float t = fadeTime;
        while (t > 0)
        {
            yield return null;
            t -= Time.deltaTime / 2;
            kettleSoundAll.volume = t / fadeTime;
        }
        yield break;
    }


    // 1
    private GameObject collidingObject;
    // 2 
    private GameObject objectInHand;

    private void SetCollidingObject(Collider col)
    {

        Debug.LogFormat("[DUPA] SetCollidingObject 1 collidingObject: {0}", !!collidingObject);


        Debug.LogFormat("[DUPA] Rigidbody present: {0}", col.GetComponent<Rigidbody>() != null);
        // 1
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = col.gameObject;

        Debug.LogFormat("[DUPA] SetCollidingObject 2 collidingObject: {0}", !!collidingObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!LZWPlib.Core.Instance.isServer)
            return;

        Debug.LogFormat("[DUPA] OnTriggerEnter {0}", other.name);

        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        if (!LZWPlib.Core.Instance.isServer)
            return;


        Debug.LogFormat("[DUPA] OnTriggerStay {0}", other.name);

        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!LZWPlib.Core.Instance.isServer)
            return;


        Debug.LogFormat("[DUPA] OnTriggerExit {0}", other.name);

        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;

        Debug.LogFormat("[DUPA] OnTriggerExit set collidingObject: {0}", !!collidingObject);

    }

    private void GrabObject()
    {
        Debug.LogFormat("[DUPA] Grab object: {0}", collidingObject.name);
        objectInHand = collidingObject;
        collidingObject = null;


        Debug.LogFormat("[DUPA] objectInHandt: {0}", !!objectInHand);

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        //objectInHand.GetComponent<Rigidbody>().useGravity = false;

        //objectInHand.transform.SetParent(this.transform); //Making the cube our controller’s child so it follows exactly where the controller goes and how it turns
        //objectInHand.GetComponent<Rigidbody>().isKinematic = true; //this make the cube resistent to other forces like gravity and collisions from moving it while allowing it to still apply forces onto others

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
        Debug.LogFormat("[DUPA] Release object: {0}", objectInHand.name);
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            Debug.LogFormat("[DUPA] Fixed joint removed: {0}", objectInHand.name);

            //objectInHand.GetComponent<Rigidbody>().useGravity = true;

            objectInHand.GetComponent<Rigidbody>().velocity = velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = angularVelocity;
        }

        //objectInHand.GetComponent<Rigidbody>().isKinematic = false; //this will disable the kinematic functionality, so other forces can affect it
        //objectInHand.transform.SetParent(null); //this will remove the controller as a parent of the cube so it will no longer follow it

        //objectInHand.GetComponent<Rigidbody>().velocity = velocity;
        //objectInHand.GetComponent<Rigidbody>().angularVelocity = angularVelocity;


        if (objectInHand.CompareTag("orb"))
        {
            TimerCave.score += 1;
            orbSound.Play();
            //Destroy(objectInHand);
            RoomChanger.Instance.DestroyOrb(objectInHand.name);
        }
        if (objectInHand.CompareTag("orb2kettle"))
        {
            TimerCave.score += 1;
            orbSound.Play();
            kettleSoundAll.Play();
            //Destroy(objectInHand);
            RoomChanger.Instance.DestroyOrb(objectInHand.name);
        }
        objectInHand = null;
        Debug.LogFormat("[DUPA] objectInHandt: {0}", !!objectInHand);
    }

    Quaternion previousRotation = Quaternion.identity;
    Vector3 previousPosition = Vector3.zero;
    Vector3 angularVelocity = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    bool previousActiveBtn = false;

    // Update is called once per frame
    void Update()
    {
        if (!LZWPlib.Core.Instance.isServer)
            return;

        Quaternion itemRotation = transform.rotation;

        Quaternion deltaRotation = itemRotation * Quaternion.Inverse(previousRotation);

        previousRotation = itemRotation;

        float angle = 0.0f;
        Vector3 axis = Vector3.zero;

        deltaRotation.ToAngleAxis(out angle, out axis);

        angle *= Mathf.Deg2Rad;

        angularVelocity = axis * angle * (1.0f / Time.deltaTime);

        Vector3 deltaPosition = transform.position - previousPosition;
        previousPosition = transform.position;
        velocity = deltaPosition * (1.0f / Time.deltaTime);



        bool activeBtn = LZWPlib.LzwpTracking.Instance.flysticks[flystickIdx].fire.isActive;

        if (Input.GetKeyDown(KeyCode.Space) || (activeBtn && !previousActiveBtn))
        {
            Debug.LogFormat("[DUPA] btn - grab");
            if (collidingObject)
            {

                Debug.LogFormat("[DUPA] btn - grab 2");
                GrabObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.X) || (!activeBtn && previousActiveBtn))
        {
            Debug.LogFormat("[DUPA] btn - release");
            if (objectInHand)
            {

                Debug.LogFormat("[DUPA] btn - release 2");
                ReleaseObject();
            }
        }


        previousActiveBtn = activeBtn;

    }

    // TBD: remove?
    //void FixedUpdate()
    //{
    //    if (!LZWPlib.Core.Instance.isServer)
    //        return;

    //    if (objectInHand != null)
    //    {
    //        objectInHand.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //        objectInHand.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //    }

    //}

    //void OnGUI()
    //{
    //    if (LZWPlib.Core.Instance.isServer)
    //        GUI.Box(new Rect(10f, 20f, 400f, 50f), string.Format("collidingObject = {0}", collidingObject ? collidingObject.name : "NULL"));
    //}
}
