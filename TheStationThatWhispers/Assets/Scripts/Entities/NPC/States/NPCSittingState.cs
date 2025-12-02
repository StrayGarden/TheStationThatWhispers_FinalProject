using UnityEngine;

public class NPCSittingState : NPCBaseState
{


    //hash for sit state's blend tree
    private readonly int SitHash = Animator.StringToHash("Sit");

    private const float CrossFadeDuration = 0.2f;


    public NPCSittingState(NPCStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        //play the free look state blend tree hash
        stateMachine.Animator.CrossFadeInFixedTime(SitHash, CrossFadeDuration);
    }


    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }
}
