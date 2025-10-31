using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State currentState;

    // Update is called once per frame
    private void Update()
    {
        //currentState? checks if it's null or not then checks tick 
        currentState?.Tick(Time.deltaTime);
    }


    //this will will enter the next state if not null
    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
