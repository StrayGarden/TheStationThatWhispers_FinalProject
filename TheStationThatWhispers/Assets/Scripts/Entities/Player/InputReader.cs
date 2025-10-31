using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{


    //bool for running
    public bool IsRunning { get; private set; }

    //Vector2 for reading movement input
    public Vector2 MovementValue { get; private set; }

    public Vector2 LookValue { get; private set; }

    //Camera Toggle event
    public event Action ChangeViewEvent;

    //Interaction Event
    public event Action InteractEvent;

    //grabs the controls script generated from the input map
    private Controls controls;

    //calls on start
    void Start()
    {
        //grabs the controls script
        controls = new Controls();

        //calls reference to input reader
        controls.Player.SetCallbacks(this);

        //enables controls
        controls.Player.Enable();
    }



    public void DisableControls()
    {
        controls.Player.Disable();
    }
    public void EnableControls()
    {
        controls.Player.Enable();
    }




    //Move Event
    public void OnMove(InputAction.CallbackContext context)
    {
       MovementValue = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            IsRunning = true;

        }
        else if (context.canceled)
        {

            IsRunning = false;

        }
    }

    //stores Look vector 2 value
    public void OnLook(InputAction.CallbackContext context)
    {
        LookValue = context.ReadValue<Vector2>();
    }

    //Event for swapping camera states
    public void OnChangeView(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        ChangeViewEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        InteractEvent?.Invoke();
    }
}
