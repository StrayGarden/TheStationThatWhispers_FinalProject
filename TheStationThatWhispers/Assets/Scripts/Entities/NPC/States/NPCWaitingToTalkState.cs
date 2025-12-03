using UnityEngine;

public class NPCWaitingToTalkState : NPCBaseState
{
    //hash for idle hash
    private readonly int IdleHash = Animator.StringToHash("Idle");

    private const float CrossFadeDuration = 0.2f;

    private float timeToRespond;

    private float timeWaitToReply;

    public NPCWaitingToTalkState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        timeToRespond = Random.Range(2f, 6f);

    }

    public override void Enter()
    {
        //play idle hash
        stateMachine.Animator.CrossFadeInFixedTime(IdleHash, CrossFadeDuration);
    }



    public override void Tick(float deltaTime)
    {
        timeWaitToReply += Time.deltaTime;

        if (timeWaitToReply >= timeToRespond)
        {

            stateMachine.SwitchState(new NPCTalkingState(stateMachine));
            timeWaitToReply = 0f;
            return;
        }
    }

    public override void Exit()
    {
        timeWaitToReply = 0f;
    }
}
