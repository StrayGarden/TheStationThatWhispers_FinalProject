using UnityEngine;

public class NPCStateMachine : StateMachine
{


    //Statemachine Inspired from the GDTV Transversal Combat Course


    


    [field: Header("Conponents and Script References")]

    //Character Controller conponent
    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: Header("NPC Movement")]

    //Movement Speed variable for free look state
    [field: SerializeField] public float WalkMovementSpeed { get; private set; }

    [field: SerializeField] public float RunMovementSpeed { get; private set; }


    [field: Header("AI Behavior")]

    [field: SerializeField] NPCStartingState StateToStart;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
        
        switch (StateToStart)
        {


            case NPCStartingState.Idle:

                Debug.Log("Starting with Idle");

                SwitchState(new NPCIdleState(this));

                break;


            case NPCStartingState.Roam:

                Debug.Log("Starting with Roaming");

                SwitchState(new NPCIdleState(this));


                break;



            case NPCStartingState.Talk:


                Debug.Log("Starting with Talking");

                SwitchState(new NPCIdleState(this));

                break;

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum NPCStartingState
{
    Idle,
    Roam,
    Talk
}
