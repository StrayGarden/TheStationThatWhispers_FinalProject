using UnityEngine;


public class NPCIdleState : NPCBaseState
{


    //hash for free look state's blend tree
    private readonly int IdleHash = Animator.StringToHash("Idle");

    private const float CrossFadeDuration = 0.2f;


    public NPCIdleState(NPCStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        //play the free look state blend tree hash
        stateMachine.Animator.CrossFadeInFixedTime(IdleHash, CrossFadeDuration);
    }


    public override void Tick(float deltaTime)
    {
       
    }

    public override void Exit()
    {

    }
}
