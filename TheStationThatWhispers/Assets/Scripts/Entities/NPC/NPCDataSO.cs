using UnityEngine;


//Creates menu asset for adding modular weapon scriptible object
[CreateAssetMenu(fileName = "NPCBehavior", menuName = "NPCs/New Behavior Action", order = 0)]
public class NPCDataSO : ScriptableObject
{


    //stores references to animation override
    [SerializeField] AnimatorOverrideController animationOverrideController = null;

    [field: SerializeField] public RoamingData[] RoamingBehavior { get; private set; }



    public void OverrideAnimator(Animator animator)
    {




        if (animationOverrideController != null)
        {
            animator.runtimeAnimatorController = animationOverrideController;
        }
        else 
        {
            Debug.Log("Animator Override Controller is null");

        }
    }

    public void CreateRoamPaths(GameObject[] NPCRoamPoints)
    {

        

        for (int i = 0; i < RoamingBehavior.Length; i++)
        {

            GameObject roamPoints = RoamingBehavior[i].RoamPoints;

            NPCRoamPoints[i] = roamPoints;

            NPCRoamPoints[i] = Instantiate(roamPoints);

            
        }



    }
}
