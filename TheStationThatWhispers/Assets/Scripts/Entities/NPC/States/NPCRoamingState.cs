using UnityEngine;
using UnityEngine.AI;

public class NPCRoamingState : NPCBaseState
{


    //Animation
    private readonly int NPCRoamingBlendTreeHash = Animator.StringToHash("NPCRoamingBlendState");

    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.2f;

    //Destination and point counter
    private GameObject roamData;

    //private GameObject selectedPath;

    private Transform[] patrolPoints;

    private int currentPointIndex = 0;


    public NPCRoamingState(NPCStateMachine stateMachine, int roamDataIndex) : base(stateMachine)
    {


        roamData = stateMachine.RoamPoints[roamDataIndex];

        roamData.gameObject.SetActive(true);

    }

    public override void Enter()
    {


        //play the free look state blend tree hash
        stateMachine.Animator.CrossFadeInFixedTime(NPCRoamingBlendTreeHash, CrossFadeDuration);

        stateMachine.Agent.autoBraking = false;


        currentPointIndex = 0;


        int count = roamData.transform.childCount;


        patrolPoints = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            patrolPoints[i] = roamData.transform.GetChild(i);
        }

        Debug.Log("In Roamming state: " + count);



        SetNextPoint();

    }



    public override void Tick(float deltaTime)
    {


        //Debug.Log("Test");


        if (roamData == null || patrolPoints.Length < 0 )
        {
            Debug.Log("Roam Data is null");
            return;
        }


        if (IsInDetectionRange())
        {

            if (stateMachine.NPCRigManager.rigHeadWeight != 1f)
            {
                stateMachine.NPCRigManager.FocusOnPlayer();
                Debug.Log("Player is in Range");
            }
            
        }
        else if (!IsInDetectionRange()) 
        {

            if (stateMachine.NPCRigManager.rigHeadWeight != 0f)
            {
                stateMachine.NPCRigManager.UnFocusFromPlayer();
            }

                
        }


        if (!stateMachine.Agent.pathPending && stateMachine.Agent.remainingDistance < 1f)
        {

            //Add to next point
            currentPointIndex++;



            if (currentPointIndex > patrolPoints.Length)
            {
                Debug.Log("Changing Roam State");
                stateMachine.SwitchState(new NPCRoamingState(stateMachine, Random.Range(0, stateMachine.NPCData.RoamingBehavior.Length)));
            }

            Debug.Log("About to move to point");

            SetNextPoint();
        }


        //Debug.Log("Moving");

        MoveToPoint(deltaTime);

        FaceTowardsPoint();

        stateMachine.Animator.SetFloat(SpeedHash, 1f, CrossFadeDuration, deltaTime);
    }

    public override void Exit()
    {
        roamData.gameObject.SetActive(false);
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    void SetNextPoint()
    {

        //if (roamData.RoamPoints.Length == 0)
        //{
        //    return;
        //}




        //Debug.Log(patrolPoints[currentPointIndex].position);

        stateMachine.Agent.destination = patrolPoints[currentPointIndex].position;

        //stateMachine.Agent.destination = patrolPoints[currentPointIndex].position;




        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;



    }

    private void MoveToPoint(float deltaTime)
    {

        //moves navmesh agent
        Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.WalkMovementSpeed, deltaTime);
        //}

        //makes sure the nav mesh agent stays in sync with the character controller
        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }

    protected void FaceTowardsPoint()
    {


        //variable for the distance between the player and the target who is accessing this
        Vector3 lookPos = stateMachine.Agent.destination - stateMachine.transform.position;

        //ignores the y value
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

}