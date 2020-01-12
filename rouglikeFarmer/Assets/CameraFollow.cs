
using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    private Transform player;
    private bool IsChasingFollow;
    private Transform ChasingPosition;
    

    private void Awake()
    {
       
    }

    public void LookAt(Vector3 LookAtPosition)
    {
        camera.transform.position = LookAtPosition;
    }

    public void Follow(Transform FollowPosition)
    {
        IsChasingFollow = true;
        ChasingPosition = FollowPosition;
    }

    public void StopFollow()
    {
        camera.Follow = null;
    }

    private void Update()
    {
        if (IsChasingFollow)
        {
            Vector3 nextPos = Vector3.Lerp(camera.transform.position, new Vector3(ChasingPosition.position.x,ChasingPosition.position.y,-11f), 10f*Time.deltaTime);
            camera.transform.position = nextPos;
            if (Vector2.Distance(camera.transform.position, ChasingPosition.position) <= 0.5f)
            {
                camera.Follow = ChasingPosition;
                IsChasingFollow = false;
            }
        }
    }
}
