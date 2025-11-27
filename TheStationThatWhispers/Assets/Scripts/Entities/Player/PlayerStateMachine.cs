using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerStateMachine : StateMachine
{

    //Statemachine Inspired from the GDTV Transversal Combat Course

    [field: Header("Conponents and Script References")]
    [field: SerializeField] public InputReader InputReader { get; private set; }

    //Character Controller conponent
    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: Header("Player Movement")]


    

    //Movement Speed variable for free look state
    [field: SerializeField] public float WalkMovementSpeed { get; private set; }

    [field: SerializeField] public float RunMovementSpeed { get; private set; }

    [field: SerializeField] public float JumpForce { get; private set; }


    [field: Header("Interacting")]


    [field: SerializeField] public float InteractionSphereCastRadius { get; private set; }

    [field: SerializeField] public float InteractionMaxDistance { get; private set; }

    [field: SerializeField] public Transform StartRayCastTransformPoint { get; private set; }

    [field: Header("Camera References")]
    
    //Camera pivot and Main camera

    [field: SerializeField] public Transform FirstPersonCameraTransform { get; private set; }

    [field: SerializeField] public Transform MainCameraTransform { get; private set; }


    //Bool for is using first person camera
    public bool FirstPersonCameraView = true;


    [field: Header("First Person Mode")]

    //Camera Rotation Speed in first person
    [field: SerializeField] public float CameraRotationSpeed { get; private set; }

    //clamping the players rotation
    [field: SerializeField] public float TopClamp { get; private set; } = 90.0f;

    [field: SerializeField] public float BottomClamp { get; private set; } = -90.0f;

    private float cinemachineTargetPitch;

    private float rotationVelocity;
    private const float threshold = 0.01f;

    //[field: Header("Third Person Mode")]

    ////character rotation speed
    //[field: SerializeField] public float CharacterRotationDamping { get; private set; }


    [field: Header("Debugger")]

    [field: SerializeField] public bool CurrentStateDebug { get; private set; } = false;



    // Start is called before the first frame update
    void Start()
    {
        //Gets Camera with MainCamera tag
        MainCameraTransform = Camera.main.transform;

        //locks cursor to screen
        Cursor.lockState = CursorLockMode.Locked;

        //makes free look the default state
        SwitchState(new FPPlayerLocomotionState(this));

    }


    //Will Update on every physics frame
    private void LateUpdate()
    {

        if(FirstPersonCameraView)
        {
            //Camera Logic is decoupled from third person update should work
            FirstPersonCameraRotation();
        }
        
    }


    //Handles Rotation of Camera in First Person
   
    protected void FirstPersonCameraRotation()
    {
        // checks for look value input
        if (InputReader.LookValue.sqrMagnitude >= threshold)
        {
  

            

            cinemachineTargetPitch -= InputReader.LookValue.y * CameraRotationSpeed * Time.deltaTime;
            rotationVelocity = InputReader.LookValue.x * CameraRotationSpeed * Time.deltaTime;

            // clamp our pitch rotation
            cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, BottomClamp, TopClamp);

            // Update Cinemachine camera target pitch
            FirstPersonCameraTransform.transform.localRotation = Quaternion.Euler(cinemachineTargetPitch, 0.0f, 0.0f);


            
            // rotate the player left and right
            transform.Rotate(Vector3.up * rotationVelocity);
        }
    }


    //passes through the camera clamp angles
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }


}
