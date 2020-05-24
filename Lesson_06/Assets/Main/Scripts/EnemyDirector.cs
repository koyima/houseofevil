using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    ZombieController[] Zombies;

    // Start is called before the first frame update
    void Start()
    {
        Zombies = GetComponentsInChildren<ZombieController>();
    }

    public void WakeUpZombies()
    {
        for (int i = 0; i < Zombies.Length; i++)
        {
            Zombies[i].WakeUpZombie();
        }
    }
    
}
