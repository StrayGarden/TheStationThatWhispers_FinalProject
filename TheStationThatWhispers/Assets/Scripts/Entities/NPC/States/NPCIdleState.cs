using UnityEngine;


public class NPCIdleState : NPCBaseState
{


    //hash for idle hash
    private readonly int IdleHash = Animator.StringToHash("Idle");

    private const float CrossFadeDuration = 0.2f;


    public NPCIdleState(NPCStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        //play idle hash
        stateMachine.Animator.CrossFadeInFixedTime(IdleHash, CrossFadeDuration);


        Debug.Log("Entered Idle");

        // stateMachine.SwitchState(new NPCSittingState(stateMachine));
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
        Debug.Log("Exiting");
    }
}
