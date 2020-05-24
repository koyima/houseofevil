using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    EnemyDirector enemyDirector;

    NavMeshAgent thisAgent;
    Animator thisAnimator;

    public bool PlayerDetected = false;
    public Transform Player;

    float AttackDistance = 0.463f;
    bool AttackCalled = false;

    public int Health = 100;
    int MaxHealth = 100;

    float WalkSpeed = 0;
    float MaxWalkSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        enemyDirector = transform.parent.GetComponent<EnemyDirector>();

        thisAgent = GetComponent<NavMeshAgent>();
        thisAnimator = GetComponent<Animator>();

        PlayerDetected = false;
        Player = GameObject.Find("Brian").transform;

        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            return;
        }

        if (PlayerDetected && !AttackCalled)
        {  
            if (Vector3.Distance(transform.position, Player.position) <= AttackDistance)
            {
                thisAnimator.SetFloat("Speed", 0);

                if (AttackCalled == false)
                {
                    thisAgent.isStopped = true;
                    thisAnimator.SetTrigger("Attack");
                    Player.GetComponent<PlayerController>().BeingAttacked();
                    AttackCalled = true;
                }                
            }
            else
            {
                thisAgent.SetDestination(Player.position);
                if (WalkSpeed <= MaxWalkSpeed)
                {
                    WalkSpeed += Time.deltaTime * 2;
                }
               
                thisAnimator.SetFloat("Speed", WalkSpeed);
            }            
        }
        else 
        {
            thisAnimator.SetFloat("Speed", 0);
        }
    }

    public void OnHit( int Damage )
    {        
        Health -= Damage;

        if (Health <= 0)
        {
            thisAgent.isStopped = true;
            thisAgent.enabled = false;
            transform.GetComponent<Collider>().enabled = false;
            thisAnimator.SetTrigger("Fall");
        }
        else
        {
            thisAnimator.SetTrigger("Hit");
            enemyDirector.WakeUpZombies();

        }
    }

    public void WakeUpZombie()
    {
        if (Health > 0)
        {
            PlayerDetected = true;
        }        
    }

    public void EnableCollider()
    {

    }

    public void DisableCollider()
    {
        transform.GetComponent<Collider>().enabled = false;
    }
}
