using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesParticles : MonoBehaviour
{
    void Awake()
    {

        gameObject.GetComponent<ParticleSystem>().Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
