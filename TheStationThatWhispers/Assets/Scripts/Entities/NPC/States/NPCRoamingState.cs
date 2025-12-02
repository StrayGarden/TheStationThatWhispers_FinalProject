using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPCRoamingState : NPCBaseState
{


    //Animation
    private readonly int NPCRoamingBlendTreeHash = Animator.StringToHash("NPCRoamingBlendState");

    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.2f;

    //Destination and point counter
    private RoamingData roamData;

    //private Transform[] patrolPoints;

    private int destinationPoint = 0;


    public NPCRoamingState(NPCStateMachine stateMachine, int roamDataIndex) : base(stateMachine)
    { 

        roamData = stateMachine.NPCData.RoamingBehavior[roamDataIndex];

    }
     
    public override void Enter()
    {
        //play the free look state blend tree hash
        stateMachine.Animator.CrossFadeInFixedTime(NPCRoamingBlendTreeHash, CrossFadeDuration);

        stateMachine.Agent.autoBraking = false;

        MoveToNextPoint();

    }

  

    public override void Tick(float deltaTime)
    {

        if (roamData == null)
        {
            Debug.Log("Roam Data is null");
            return;
        }

        if (stateMachine.NPCData.RoamingBehavior.Length == -1)
        {

            Debug.Log("Not Enough Point");
            return;
        }

        if (roamData.RoamPoints.Length >= destinationPoint)
        {
            stateMachine.SwitchState(new NPCRoamingState(stateMachine, Random.Range(0, stateMachine.NPCData.RoamingBehavior.Length)));
        }

        if (!stateMachine.Agent.pathPending && stateMachine.Agent.remainingDistance > 0.5f)
        {
            MoveToNextPoint();
        }

        MoveToPoint(deltaTime);

        stateMachine.Animator.SetFloat(SpeedHash, 1f, CrossFadeDuration, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    void MoveToNextPoint()
    {

        if (roamData.RoamPoints.Length == 0)
        {
            return;
        }

        stateMachine.Agent.destination = roamData.RoamPoints[destinationPoint].position;


        destinationPoint = (destinationPoint + 1) % roamData.RoamPoints.Length;



    }

    private void MoveToPoint(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            ////turns nav mesh target to be the players transform
            //stateMachine.Agent.destination = stateMachine.PlayerObject.transform.position;

            //moves navmesh agent
            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.WalkMovementSpeed, deltaTime);
        }

        //makes sure the nav mesh agent stays in sync with the character controller
        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }
}
