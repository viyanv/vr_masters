using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour {

    public GameObject[] levels;
    public Vector3[] positions;
    public Vector3[] rotations;
    public Transform rigVR;

    

    public static RoomChanger Instance;

	// Use this for initialization
	void Start () {
        RoomChanger.Instance = this.GetComponent<RoomChanger>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void jakas_metoda(int index)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i==index);
        }
        rigVR.position = positions[index];
        rigVR.rotation = Quaternion.Euler(rotations[index]);
        
        
    }
}
