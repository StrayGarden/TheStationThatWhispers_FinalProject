using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//FIRST PERSON LOCOMOTION STATE
public class FPPlayerLocomotionState : PlayerBaseState
{


    //turns the string into a interger to make it faster
    //hash for look speed
    private readonly int MoveSpeedZHash = Animator.StringToHash("MoveDirectionZ");

    private readonly int MoveSpeedYHash = Animator.StringToHash("MoveDirectionY");

    //hash for free look state's blend tree
    private readonly int FPFreeLookBlendTreeHash = Animator.StringToHash("FPFreeLookBlendTree");

    private const float AnimatorDampTime = 0.1f;

    //time to transition from states
    private const float CrossFadeDuration = 0.3f;


    public FPPlayerLocomotionState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {


        stateMachine.FirstPersonCameraView = true;

        //play the free look state blend tree hash
        stateMachine.Animator.CrossFadeInFixedTime(FPFreeLookBlendTreeHash, CrossFadeDuration);


        //subscribes to Jump action
        stateMachine.InputReader.JumpEvent += OnJump;

        stateMachine.InputReader.InteractEvent += OnInteractable;


    }

  

    public override void Tick(float deltaTime)
    {
        //calculates gravity
        //Gravity();


        //debugs current state
        if (stateMachine.CurrentStateDebug)
        {
            Debug.Log("Walk State");
        }



        //gets the input values from the vector 3 movement
        Vector3 movement = CalculateMovement();

        //accesses the move method on the character controller(moving by movement vector 3 * FreeLookMovementSpeed * real time)
        Move(movement * stateMachine.WalkMovementSpeed, deltaTime);


        //if run input event then going to run state
        if (stateMachine.InputReader.IsRunning)
        {
            stateMachine.SwitchState(new FPPlayerRunningState(stateMachine));
        }

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(MoveSpeedZHash, 0, AnimatorDampTime, deltaTime);

            stateMachine.Animator.SetFloat(MoveSpeedYHash, 0, AnimatorDampTime, deltaTime);


            return;

        }

        //breaking down movement value vector2 into two floats for the move direction
        float moveDirectionX = stateMachine.InputReader.MovementValue.x;

        float moveDirectionY = stateMachine.InputReader.MovementValue.y;

        stateMachine.Animator.SetFloat(MoveSpeedZHash, moveDirectionX, AnimatorDampTime, deltaTime);

        stateMachine.Animator.SetFloat(MoveSpeedYHash, moveDirectionY, AnimatorDampTime, deltaTime);

    }

    public override void Exit()
    {

        //subscribes button to change camera event
        //stateMachine.InputReader.ChangeViewEvent -= ChangeToThirdPerson;

        stateMachine.InputReader.JumpEvent -= OnJump;

        stateMachine.InputReader.InteractEvent -= OnInteractable;

    }

    private void OnJump()
    {
        stateMachine.SwitchState(new FPPlayerJumpState(stateMachine));
    }

    private void OnInteractable()
    {
        throw new NotImplementedException();
    }

    //private void ChangeToThirdPerson()
    //{
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
