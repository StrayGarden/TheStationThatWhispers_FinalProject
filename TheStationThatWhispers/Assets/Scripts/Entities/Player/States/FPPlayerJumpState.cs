using UnityEngine;

public class FPPlayerJumpState : PlayerBaseState
{


    private readonly int JumpHash = Animator.StringToHash("Jump");

    //time to transition from states
    private const float CrossFadeDuration = 0.3f;

    private Vector3 momentum;

    public FPPlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //makes the player ability to jump vertically
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);

        //momentum and can move the character except for y
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        //play the Jump hash
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);
    }

   

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        //gets the input values from the vector 3 movement
        Vector3 movement = CalculateMovement();
        movement.y = 0f;
        Move(movement * stateMachine.WalkMovementSpeed, deltaTime);


        //checks if the characters velocity equals or is less than 0
        if (stateMachine.Controller.velocity.y <= 0f)
        {
            //transitions to falling state
            stateMachine.SwitchState(new FPPlayerFallState(stateMachine));

            return;
        }
    }


    public override void Exit()
    {
        
    }


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
