using UnityEngine;
using UnityEngine.Animations.Rigging;
using System;

public class NPCIKRigManager : MonoBehaviour
{
    
   

    [field: SerializeField] public float rigHeadWeight { get; private set; }

    [field: SerializeField] public MultiAimConstraint HeadIKRig { get; private set; }

    //
    public void FocusOnPlayer()
    {
        rigHeadWeight = 1f;

        HeadIKRig.weight = rigHeadWeight;

    }

    public void UnFocusFromPlayer()
    {
        rigHeadWeight = 0f;

        HeadIKRig.weight = rigHeadWeight;

    }



}
