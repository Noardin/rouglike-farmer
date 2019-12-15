using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth : PickUps
{
    public double HpRestoreNumber = 1;
    private Health_Manager HealthManager;

    protected override void Start()
    {
        base.Start();
        HealthManager = GameObject.Find("HealthManager").GetComponent<Health_Manager>();
    }
    protected override void CollectPickUp()
    {
        base.CollectPickUp();
        restoreHP(HpRestoreNumber);
    }

    void restoreHP(double HP)
    {
        HealthManager.Heal(HP);

    }
}
