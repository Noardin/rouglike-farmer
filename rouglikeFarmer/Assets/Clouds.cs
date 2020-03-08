using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Clouds : MonoBehaviour
{
    public GameObject[] clouds;
    private List<Transform> ActiveClouds = new List<Transform>();
    public float speed;
    public float SpawnDelay;
    private float Timer;
    private Transform endpoint;
    private Transform startpoint;
    
    // Start is called before the first frame update
    void Start()
    {
        Timer = SpawnDelay;
        endpoint = GameObject.Find("CloudEndPoint").transform;
        startpoint = GameObject.Find("CloudStartPoint").transform;
        var r = 0;
        foreach (GameObject c in clouds)
        {
            //start displacement
            float d = Vector2.Distance(startpoint.position, endpoint.position) / (clouds.Length*2) * r;
            GameObject i =Instantiate(c, new Vector2(startpoint.position.x + d, startpoint.position.y) , Quaternion.identity);
            i.transform.parent = transform.parent;
            ActiveClouds.Add(i.transform);
            r++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ActiveClouds.Count; i++)
        {
            ActiveClouds[i].position = Vector2.MoveTowards(new Vector2(ActiveClouds[i].position.x, ActiveClouds[i].position.y), endpoint.position,
                Time.deltaTime * speed * Mathf.Clamp(i, 1,  3));
            if (Vector2.Distance(ActiveClouds[i].position, endpoint.position) <= 1)
            {
                Destroy(ActiveClouds[i].gameObject);
                ActiveClouds.RemoveAt(i);
            }
        }

        if (Timer <= 0)
        {
            GameObject i =Instantiate(clouds[Random.Range(0, clouds.Length)], startpoint.position, Quaternion.identity);
            i.transform.parent = transform.parent;
            ActiveClouds.Add(i.transform);
            Timer = SpawnDelay;
        }

        Timer -= Time.deltaTime;

    }
}
