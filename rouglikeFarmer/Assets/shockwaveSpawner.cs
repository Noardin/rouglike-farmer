using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockwaveSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public int shockwaveAmount = 3;
    private int shockwaveCount;
    private float timeBetween;
    public float shockwaveDelay = 0.2f;
    public GameObject Wave;
    public float shockWaveSpeed;
    public float MaxExpantion;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn(float shockWaveSpeed, float MaxExpantion, float shockWaveDelay, int shockWaveAmount)
    {
        this.shockWaveSpeed = shockWaveSpeed;
        this.MaxExpantion = MaxExpantion;
        shockwaveDelay = shockWaveDelay;
        shockwaveAmount = shockWaveAmount;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        if (shockwaveCount < shockwaveAmount)
        {
            timeBetween = shockwaveDelay;
            shockwaveCount++;
            GameObject shockWave = Instantiate(Wave, gameObject.transform.position, Quaternion.identity);
            ShockWaveScript script = shockWave.GetComponent<ShockWaveScript>();
            script.shockwaveSpeed = shockWaveSpeed;
            script.MaxExpantion = MaxExpantion;
            
            
            yield return new WaitForSeconds(shockwaveDelay);

            StartCoroutine(SpawnWave());
        }
        else
        {
            shockwaveCount = 0;
        }
        
    }

}
