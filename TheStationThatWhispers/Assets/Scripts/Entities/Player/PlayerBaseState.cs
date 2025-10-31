using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{



    protected PlayerStateMachine stateMachine;

    //this is a constructor
    public PlayerBaseState(PlayerStateMachine stateMachine)
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


    //protected RaycastHit[] SphereCastForwared()
    //{
    //    float radius = stateMachine.InteractionSphereCastRadius;
    //    float distance = stateMachine.InteractionMaxDistance;
    //    Vector3 direction = Camera.main.transform.forward;
    //    Vector3 startingPosition = stateMachine.RayCastStartPostitionTransform.position;

    //    return Physics.SphereCastAll(startingPosition, radius, direction, distance);
    //}

    ////method that will return the Camera state // Buggy but good logic but needs better implementation
    //protected void SwitchCameraStyle()
    //{
    //    if (stateMachine.SwitchCameraView == true)
    //    {
    //        stateMachine.SwitchState(new FPPlayerLocomotionState(stateMachine));
    //    }
    //    else
    //    {
    //        stateMachine.SwitchState(new TPPlayerFreeLookState(stateMachine));
    //    }
    //}


}
