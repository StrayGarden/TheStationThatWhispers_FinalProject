using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{


    private Camera mainCam;
    [SerializeField] CinemachineCamera firstPersonCMCam;


    //Camera Values'

    private float nearClipPlaneAmount = 0.01f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main;

        firstPersonCMCam.Lens.NearClipPlane = nearClipPlaneAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
