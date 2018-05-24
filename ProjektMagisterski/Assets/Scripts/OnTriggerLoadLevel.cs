using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerLoadLevel : MonoBehaviour {

    [SerializeField] private string loadLevel;

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetButtonDown("Use"))
            {
                SceneManager.LoadScene(loadLevel);
            }
        }
    }

}
