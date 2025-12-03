using UnityEngine;

public class NPCTalkingState : NPCBaseState
{

    //hash for talking hash
    private int TalkHash = Animator.StringToHash("Talk");

    private const float CrossFadeDuration = 0.2f;


    private float talkInterval;

    private float timeWhileTalking;

    public NPCTalkingState(NPCStateMachine stateMachine) : base(stateMachine)
    {

        string animationToPlay = "Talk" + Random.Range(1, 3);

        TalkHash = Animator.StringToHash(animationToPlay);

        //Debug.Log(TalkHash);

        talkInterval = Random.Range(2f, 8f);

    }

    public override void Enter()
    {
        //play idle hash
        stateMachine.Animator.CrossFadeInFixedTime(TalkHash, CrossFadeDuration);
    }

    

    public override void Tick(float deltaTime)
    {
        timeWhileTalking += Time.deltaTime;

        if (timeWhileTalking >= talkInterval)
        {

            stateMachine.SwitchState(new NPCWaitingToTalkState(stateMachine));
            timeWhileTalking = 0f;
            return;
        }
    }

    public override void Exit()
    {
        timeWhileTalking = 0f;
    }
}
