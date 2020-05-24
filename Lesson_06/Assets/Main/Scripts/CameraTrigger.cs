using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    CameraController mainCameraController;

    public int CameraID;


    // Start is called before the first frame update
    void Start()
    {
        mainCameraController = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainCameraController.ActivateCamera(CameraID);
        }
    }
}
