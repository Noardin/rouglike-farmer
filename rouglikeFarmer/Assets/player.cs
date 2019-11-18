using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Health_Manager healthManager;
    public Transform position;
    private void Awake()
    {
        healthManager = GetComponentInChildren<Health_Manager>();
        position = transform;
    }
    public void TakeDamage(double damage)
    {
        healthManager.TakeDamage(damage);
    }


}
