using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ShockWaveScript : MonoBehaviour
{
    public float shockwaveSpeed = 0.04f;
    public float MaxExpantion = 2f;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(shockwaveSpeed,  shockwaveSpeed, 0f);
        
        if(transform.localScale.x >= MaxExpantion)
        {
            Debug.Log("destroy... x:"+ transform.localScale.x);
            Destroy(gameObject);
        }
        
    }
}
