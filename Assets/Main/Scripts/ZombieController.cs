using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    NavMeshAgent thisAgent;
    Animator thisAnimator;

    public bool PlayerDetected = false;
    public Transform Player;

    float AttackDistance = 0.463f;
    bool AttackCalled = false;

    // Start is called before the first frame update
    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        thisAnimator = GetComponent<Animator>();

        PlayerDetected = false;
        Player = GameObject.Find("Brian").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDetected && !AttackCalled)
        {  
            if (Vector3.Distance(transform.position, Player.position) <= AttackDistance)
            {
                thisAnimator.SetFloat("Speed", 0);

                if (AttackCalled == false)
                {
                    thisAnimator.SetTrigger("Attack");
                    Player.GetComponent<PlayerController>().BeingAttacked();
                    AttackCalled = true;
                }                
            }
            else
            {
                thisAgent.SetDestination(Player.position);
                thisAnimator.SetFloat("Speed", 1);
            }            
        }
        else 
        {
            thisAnimator.SetFloat("Speed", 0);
        }
    }
}
