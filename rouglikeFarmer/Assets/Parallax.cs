using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Transform[] backgrounds;

    private float[] parallaxScales;

    public float smoothing = 1f; // >0 

    [HideInInspector]public Transform cam;

    private Vector3 previousCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * -parallaxScales[i];
            float parallaxY = (previousCamPos.y - cam.position.y) * -parallaxScales[i]/2;

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            float backgroundTargetPosY = backgrounds[i].position.y + parallaxY;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);
            
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing*Time.fixedDeltaTime);
        }

        previousCamPos = cam.position;
    }
}
