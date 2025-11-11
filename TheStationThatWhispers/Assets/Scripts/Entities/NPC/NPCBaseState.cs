using UnityEngine;

public abstract class NPCBaseState : State
{
    protected NPCStateMachine stateMachine;

    //this is a constructor
    public NPCBaseState(NPCStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
