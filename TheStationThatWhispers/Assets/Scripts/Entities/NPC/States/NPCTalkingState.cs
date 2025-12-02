using UnityEngine;

public class NPCTalkingState : NPCBaseState
{
    public NPCTalkingState(NPCStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering");
    }

    

    public override void Tick(float deltaTime)
    {
        Debug.Log("Ticking");
    }

    public override void Exit()
    {
        Debug.Log("Exiting");
    }
}
