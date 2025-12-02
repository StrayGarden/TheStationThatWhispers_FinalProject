using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    //grabs the current state
    private State currentState;

    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // Update is called once per frame
    protected virtual void Update()
    {
        //currentState? checks if it's null or not then checks tick 
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState)
    {
        //checks if current state is not null then can use exit
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
