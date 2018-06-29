using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{

    public GameObject[] levels;
    public Vector3[] positions;
    public Vector3[] rotations;
    public Transform rigVR;

    //GameObject[] orbs;
    //public GameObject orb2kettle;




    public static RoomChanger Instance;

    NetworkView nv;

    private void Awake()
    {
        nv = GetComponent<NetworkView>();
    }

    // Use this for initialization
    void Start()
    {
        RoomChanger.Instance = this.GetComponent<RoomChanger>();

       // orbs = GameObject.FindGameObjectsWithTag("orb");

    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (LZWPlib.Core.Instance.isServer && Input.GetKeyDown(KeyCode.R))
    //        jakas_metoda(1);
    //}

    public void jakas_metoda(int index)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == index);
        }
        rigVR.position = positions[index];
        rigVR.rotation = Quaternion.Euler(rotations[index]);

        nv.RPC("jakas_metodaRPC", RPCMode.Others, index);
    }

    [RPC]
    public void jakas_metodaRPC(int index)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == index);
        }
    }



    public void DestroyOrb(string orbName)
    {
        Debug.LogFormat("Destroy orb: {0}", orbName);
        nv.RPC("DestroyOrbRPC", RPCMode.All, orbName);
    }

    [RPC]
    void DestroyOrbRPC(string orbName)
    {
        GameObject go = GameObject.Find(orbName);
        if (go)
            Destroy(go);
    }
}
