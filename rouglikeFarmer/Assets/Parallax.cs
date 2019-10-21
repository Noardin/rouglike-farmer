﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Transform[] backgrounds;

    private float[] parallaxScales;

    public float smoothing = 1f; // >0 

    public Transform cam;

    private Vector3 previousCamPos;


 
    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;
        Debug.Log("camx:" + cam.position.x);

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

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing*Time.deltaTime);
        }

        previousCamPos = cam.position;
    }
}
