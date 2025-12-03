using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using System;

public class NPCIKRigManager : MonoBehaviour
{
    
   

    [field: SerializeField] public float rigHeadWeight { get; private set; }

    //Gets head IK Rig 
    [field: SerializeField] public MultiAimConstraint HeadIKRig { get; private set; }



    private void Start()
    {


        //
        if (HeadIKRig  == null)
        {
            return;
        }

        //Creates new WeightedTransformArray to add the player camera to the array
        var headIKSources = new WeightedTransformArray();

        headIKSources.Add(new WeightedTransform(PlayerStateMachine.Instance.FirstPersonCameraTransform, 1f));

        HeadIKRig.data.sourceObjects = headIKSources;

    }

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
