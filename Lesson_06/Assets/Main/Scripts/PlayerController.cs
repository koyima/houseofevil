using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator thisAnimator;
    CharacterController thisCharacterController;
    AudioSource thisAudioSource;

    float InputVertical;
    float InputHorizontal;

    float Sprint = 1;
    float SprintMax = 2;
    float SprintMotion = 4;
    float SprintMotionMax = 4;    

    public float Speed = 1.5f;
    public float RotationSpeed = 150f;
    private Vector3 moveDirection = Vector3.zero;

    // Damage / Health
    bool isAlive = true;

    bool beingAttacked = false;
    public float Health = 100;
    float MaxHealth = 100;

    // Shooting - Don't forget to set GunNozzle at the tip of the gun
    bool aiming = false;
    public Transform GunNozzle;
    public LayerMask Shootables;
    public float ShootTolerance = 0.25f;

    public int CurrentClip = 13;
    public int MaxClip = 13;
    public int MaxAmmo = 200;

    // Audio 
    public AudioClip GunShoot;

    // Start is called before the first frame update
    void Start()
    {      
        thisAnimator = GetComponent<Animator>();
        thisCharacterController = GetComponent<CharacterController>();
        thisAudioSource = GetComponent<AudioSource>();

        Health = MaxHealth;

        CurrentClip = MaxClip;
        thisAnimator.SetInteger("CurrentClip", CurrentClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == false)
        {
            return;
        }

        if (beingAttacked)
        {            
            return;
        }

        

        if (Input.GetButton("Fire2"))
        {
            if (Input.GetButtonDown("Fire3"))
            {
                if (MaxAmmo > 0)
                {
                    if (CurrentClip < MaxClip)
                    {
                        thisAnimator.SetTrigger("Reload");
                    }
                }
            }
            else
            {
                aiming = true;
                thisAnimator.SetBool("Aiming", true);
            }
        }
        else
        {
            aiming = false;
            thisAnimator.SetBool("Aiming", false);
        }

        if (aiming)
        {
            InputVertical = Input.GetAxis("Vertical");
            InputHorizontal = Input.GetAxis("Horizontal");

            // Set Animation
            thisAnimator.SetFloat("Vertical", InputVertical );

            // Set Motion
            transform.Rotate(0, InputHorizontal * RotationSpeed * Time.deltaTime, 0);

            // Shoot
            if (Input.GetButtonDown("Fire1"))
            {
                if (CurrentClip > 0)
                {
                    RaycastHit hit;
                    if (Physics.SphereCast(GunNozzle.position - GunNozzle.forward * 0.6f, ShootTolerance, GunNozzle.forward, out hit, 100, Shootables))
                    {
                        hit.transform.GetComponent<ZombieController>().OnHit(35);
                    }
                    thisAnimator.SetTrigger("Shoot");
                    thisAudioSource.PlayOneShot(GunShoot);
                    CurrentClip--;
                    thisAnimator.SetInteger("CurrentClip", CurrentClip);
                }
                else
                {
                    if (MaxAmmo > 0)
                    {
                        thisAnimator.SetTrigger("Reload");
                    }
                    else
                    {
                        // No more ammo, play gun empty sound
                    }
                }
            }
            
            return;
        }

        // Get Input
        InputVertical = Input.GetAxis("Vertical");
        InputHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Sprint < SprintMax)
            {
                Sprint += Time.deltaTime;
            }

            if (SprintMotion <SprintMotionMax)
            {
                SprintMotion += Time.deltaTime * 2;
            }
        }
        else
        {
            if (Sprint > 1)
            {
                Sprint -= Time.deltaTime;
            }

            if (SprintMotion > 1)
            {
                SprintMotion -= Time.deltaTime * 2;
            }
        }

        // Set Animation
        thisAnimator.SetFloat("Vertical", InputVertical * Sprint);

        // Set Motion
        transform.Rotate(0, InputHorizontal * RotationSpeed * Time.deltaTime, 0);

        moveDirection = transform.forward * InputVertical;
        moveDirection *= Speed * SprintMotion;

        thisCharacterController.Move(moveDirection * Time.deltaTime);
        
    }

    public void BeingAttacked()
    {        
        beingAttacked = true;
        Health -= 50;
        thisAnimator.SetFloat("Health", Health);

        if (Health <= 0)
        {
            isAlive = false;
            thisAnimator.SetTrigger("Death");
        }
        else
        {
            thisAnimator.SetTrigger("Attacked");
        }
    }

    public void StoppedAttack()
    {
        beingAttacked = false;
    }

    public void ReloadFinished()
    {
        int AmmoMissing = MaxClip - CurrentClip;

        if (MaxAmmo >= AmmoMissing)
        {
            CurrentClip += AmmoMissing;
            MaxAmmo -= AmmoMissing;
        }
        else
        {
            CurrentClip = MaxAmmo;
            MaxAmmo = 0;
        }

        thisAnimator.SetInteger("CurrentClip", CurrentClip);
    }
}
