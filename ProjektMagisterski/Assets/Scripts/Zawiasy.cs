using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zawiasy : MonoBehaviour
{

    public Transform sth;
    public Vector3 correction;

    public float minZ = 0f;
    public float maxZ = 0f;

    public Vector3 v2;
    public Vector3 v3;

    // Update is called once per frame
    void Update()
    {
        //Vector3 v = sth.position;
        //v.y = transform.position.y;

        //Quaternion q = Quaternion.LookRotation(sth.position - transform.position, Vector3.forward);
        //q.x = 0f;
        //q.y = 0f;

        ////transform.localRotation = q;

        //transform.LookAt(v);
        //transform.Rotate(correction);


        //v2 = transform.localRotation.eulerAngles;
        //v3 = transform.localRotation.eulerAngles;

        //if (v2.z < 0)
        //v2.z = (v2.z + 360f);// % 360f;

        //if (v2.z < 270f) //wchodzi w szafke
        //    v2.z = 270f;

        //if (v2.z > 360f)
        //    v2.z = 360f;

        //transform.localRotation = Quaternion.Euler(v2);

    }
}
