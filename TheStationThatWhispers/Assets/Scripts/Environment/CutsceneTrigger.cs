using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneTrigger : MonoBehaviour
{

    [SerializeField] PlayableDirector cutsceneToPlay;


 
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            cutsceneToPlay.Play();

            Destroy(this);
        }

        
    }
}
