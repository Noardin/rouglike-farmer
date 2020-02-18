
using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    private Transform player;
    private bool IsChasingFollow;
    private Transform ChasingPosition;
    private float CameraDistance = 5.5f;
    private float timeLeft = 0.5f;
    

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
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
            
            
            if (timeLeft <= Time.deltaTime)
            {
                camera.Follow = player.transform;
                camera.m_Lens.OrthographicSize = CameraDistance;
                IsChasingFollow = false;
            }
            else
            {
                Vector3 nextPos = Vector3.Lerp(camera.transform.position, new Vector3(ChasingPosition.position.x,ChasingPosition.position.y,-11f),Time.deltaTime/timeLeft);
                
                float nextZoom = Mathf.Lerp(camera.m_Lens.OrthographicSize, CameraDistance, Time.deltaTime/timeLeft);
                Debug.Log("chasingFolow "+nextZoom +" "+nextPos);
                camera.m_Lens.OrthographicSize = nextZoom;
                camera.transform.position = nextPos;
                timeLeft -= Time.deltaTime;
            }
            
        }
    }
}
