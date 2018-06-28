using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRigidbodies : MonoBehaviour
{

    void Start()
    {
        if (!LZWPlib.Core.Instance.isServer)
        {
            var rbs = GameObject.FindObjectsOfType<Rigidbody>();
            foreach (var rb in rbs)
                Destroy(rb);
        }
    }

}
