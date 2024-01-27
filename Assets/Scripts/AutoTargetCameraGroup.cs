using Cinemachine;
using UnityEngine;

public class AutoTargetCameraGroup : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachine;

    void Start()
    {
        if (cinemachine == null) cinemachine = GetComponent<CinemachineVirtualCamera>();

        GameObject objetTrouve = GameObject.Find("TargetGroup");

        if (objetTrouve != null) cinemachine.Follow = objetTrouve.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
