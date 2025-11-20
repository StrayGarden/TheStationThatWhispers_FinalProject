using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{

    //Force Receiver from the GDTV Transversal Combat Course

    //stores character controller
    [SerializeField] private CharacterController controller;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float drag = 0.3f;

    private Vector3 dampingVelocity;

    private Vector3 impact;

    //variable how much to move forward
    private float verticalVelocity;

    //Vector 3 for movement from impact and moving the character forward
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        //creates gravity for the player
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

        //checks if the nav mesh agent isn't null
        if (agent != null)
        {
            //if there is no longer knockback force turns the nav mesh back on
            if (impact.sqrMagnitude < 0.2f * 0.2f)
            {
                impact = Vector3.zero;
                agent.enabled = true;
            }


        }


    }

    //function to add knockback
    public void AddForce(Vector3 force)
    {
        impact += force;

        //disables the nav mesh agent for knockback
        if (agent != null)
        {
            agent.enabled = false;
        }


    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }
}
