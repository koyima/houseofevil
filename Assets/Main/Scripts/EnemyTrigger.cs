using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    ZombieController thisZombieController;

    // Start is called before the first frame update
    void Start()
    {
        thisZombieController = transform.root.GetComponent<ZombieController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            thisZombieController.PlayerDetected = true;
        }
    }
}
