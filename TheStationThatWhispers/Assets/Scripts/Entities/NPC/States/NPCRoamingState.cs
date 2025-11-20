using UnityEngine;

public class NPCRoamingState : NPCBaseState
{

    private readonly int TalkingHash = Animator.StringToHash("Talking");

    private const float CrossFadeDuration = 0.2f;


    public NPCRoamingState(NPCStateMachine stateMachine) : base(stateMachine)
    {
         //hash for free look state's blend tree
   

    }

    public override void Enter()
    {
        //play the free look state blend tree hash
        stateMachine.Animator.CrossFadeInFixedTime(TalkingHash, CrossFadeDuration);
    }

  

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }
}
