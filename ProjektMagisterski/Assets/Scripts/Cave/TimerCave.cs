using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCave : MonoBehaviour {
    float timePassed = 0;
    public static int score = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
	}

    void OnGUI()
    {
        if (LZWPlib.Core.Instance.isServer)
            GUILayout.Label("Czas: " + (int)timePassed + "\nPunkty: " + score);
    }
}
