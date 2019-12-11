
using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    private Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        if (camera.Follow == null)
        {
            camera.Follow = player;
        }
    }
}
