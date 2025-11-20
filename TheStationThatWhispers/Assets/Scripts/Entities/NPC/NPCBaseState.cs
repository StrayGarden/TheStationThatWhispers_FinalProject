using UnityEngine;

public abstract class NPCBaseState : State
{
    protected NPCStateMachine stateMachine;

    //this is a constructor
    public NPCBaseState(NPCStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    //stores movement
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    //method for apply force in movement
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }
}
