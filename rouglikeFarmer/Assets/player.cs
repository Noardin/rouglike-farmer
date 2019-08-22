using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(double Damage)
    {
        Health_Manager healthManager = GetComponentInChildren<Health_Manager>();
        healthManager.TakeDamage(Damage);
    }


}
