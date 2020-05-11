using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] Cameras;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].SetActive(false);
        }

        Cameras[0].SetActive(true);


    }

    public void ActivateCamera (int incCamerID)
    {
        for (int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].SetActive(false);
        }

        Cameras[incCamerID].SetActive(true);
    }
}
