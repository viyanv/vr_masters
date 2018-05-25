using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureDrop : MonoBehaviour {

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.name == "PictureToDrop")
        {
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            //Destroy(col.gameObject);
            //Debug.Log(gameObject.name + " Triggered");
        }
    }

}
