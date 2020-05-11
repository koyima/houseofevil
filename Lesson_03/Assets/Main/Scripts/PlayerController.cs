using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator thisAnimator;
    CharacterController thisCharacterController;

    float InputVertical;
    float InputHorizontal;

    float Sprint = 1;
    float SprintMax = 2;
    float SprintMotion = 4;
    float SprintMotionMax = 4;
    

    public float Speed = 1.5f;
    public float RotationSpeed = 150f;
    private Vector3 moveDirection = Vector3.zero;

    bool beingAttacked = false;

    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = GetComponent<Animator>();
        thisCharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (beingAttacked)
        {
            
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
        thisAnimator.SetTrigger("Attacked");
        beingAttacked = true;
    }

    public void StoppedAttack()
    {
        beingAttacked = false;
    }
}
