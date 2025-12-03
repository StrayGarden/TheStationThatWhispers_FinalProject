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

        Debug.Log("Entered Sitting State");
    }


    public override void Tick(float deltaTime)
    {

        if (IsInDetectionRange())
        {

            if (stateMachine.NPCRigManager.rigHeadWeight != 1f)
            {
                stateMachine.NPCRigManager.FocusOnPlayer();
                Debug.Log("Player is in Range");
            }

        }
        else if (!IsInDetectionRange())
        {

            if (stateMachine.NPCRigManager.rigHeadWeight != 0f)
            {
                stateMachine.NPCRigManager.UnFocusFromPlayer();
            }


        }


        Debug.Log("Ticking");
    }

    public override void Exit()
    {
        Debug.Log("Ticking");
    }
}
