using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPlayerRunningState : PlayerBaseState
{


    private readonly int FreeLookSpeedHash = Animator.StringToHash("MoveSpeed");

    //hash for free look state's blend tree
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

    //time to transition from states
    private const float CrossFadeDuration = 0.3f;

    private const float AnimatorDampTime = 0.1f;

    public FPPlayerRunningState(PlayerStateMachine stateMachine) : base(stateMachine)
    {


    }

    public override void Enter()
    {
        //play the free look state blend tree hash
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);

        //subscribes button to change camera event
        //stateMachine.InputReader.ChangeViewEvent += ChangeToThirdPerson;
    }

    public override void Tick(float deltaTime)
    {

        //gets the input values from the vector 3 movement
        Vector3 movement = CalculateMovement();

        //accesses the move method on the character controller(moving by movement vector 3 * FreeLookMovementSpeed * real time)
        Move(movement * stateMachine.WalkMovementSpeed, deltaTime);

        //debugs current state
        if (stateMachine.CurrentStateDebug)
        {

            Debug.Log("Run State");
        }

           

        //will swutch back to idle state if no movement input
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.SwitchState(new FPPlayerLocomotionState(stateMachine));
            //stateMachine.SwitchState(new FPPlayerFreeLookState(stateMachine));
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        //subscribes button to change camera event
        //stateMachine.InputReader.ChangeViewEvent -= ChangeToThirdPerson;
    }

    //private void ChangeToThirdPerson()
    //{
    //    //subscribes button to change camera event
    //    stateMachine.SwitchState(new TPPlayerFreeLookState(stateMachine));
    //}


    //makes movement relative to the camera
    protected Vector3 CalculateMovement()
    {


        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();


        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;



    }


}
