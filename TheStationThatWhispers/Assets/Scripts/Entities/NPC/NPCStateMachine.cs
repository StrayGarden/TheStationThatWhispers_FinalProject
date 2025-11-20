using UnityEngine;
using UnityEngine.AI;

public class NPCStateMachine : StateMachine
{


    //Statemachine Inspired from the GDTV Transversal Combat Course


    


    [field: Header("Conponents and Script References")]

    //Character Controller conponent
    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public NavMeshAgent Agent { get; private set; }

    [field: Header("NPC Movement")]

    //Movement Speed variable for free look state
    [field: SerializeField] public float WalkMovementSpeed { get; private set; }

    [field: SerializeField] public float RunMovementSpeed { get; private set; }


    [field: Header("AI Behavior")]

    [field: SerializeField] NPCMainState MainNPCBehavior;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //Makes sure nav mesh can't move and rotate gameobject //better to do movement through code
        Agent.updatePosition = false;
        Agent.updateRotation = false;

        NPCStartBehavior();

    }


    // Update is called once per frame
    void Update()
    {

    }

    private void NPCStartBehavior()
    {
        switch (MainNPCBehavior)
        {


            case NPCMainState.Idle:

                Debug.Log("Starting with Idle");

                SwitchState(new NPCIdleState(this));

                break;


            case NPCMainState.Roam:

                Debug.Log("Starting with Roaming");

                SwitchState(new NPCRoamingState(this));


                break;



            case NPCMainState.Talk:


                Debug.Log("Starting with Talking");

                SwitchState(new NPCTalkingState(this));

                break;

            case NPCMainState.Sitting:


                Debug.Log("Starting with Talking");

                SwitchState(new NPCIdleState(this));

                break;

        }
    }

   
}

public enum NPCMainState
{
    Idle,
    Roam,
    Talk,
    Sitting
}
