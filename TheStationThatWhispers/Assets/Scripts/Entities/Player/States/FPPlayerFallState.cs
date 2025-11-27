using UnityEngine;

public class FPPlayerFallState : PlayerBaseState
{


    private readonly int FallHash = Animator.StringToHash("Fall");

    //time to transition from states
    private const float CrossFadeDuration = 0.3f;

    private Vector3 momentum;



    public FPPlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        //play the Fall hash
        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        //gets the input values from the vector 3 movement
        Vector3 movement = CalculateMovement();
         momentum.y = 0f;
        Move(movement * stateMachine.WalkMovementSpeed, deltaTime);

        //checks if the character controller is grounded
        if (stateMachine.Controller.isGrounded)
        {

            stateMachine.SwitchState(new FPPlayerLocomotionState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }

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
