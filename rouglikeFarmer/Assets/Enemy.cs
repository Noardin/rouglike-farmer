using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP = 100;
    public GameObject player;
    public Rigidbody2D enemybody;
    private Rigidbody2D playerbody;
    private Vector3 moveDirection;
    public float KnockBackFoce = 10f;
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        playerbody = player.GetComponent<Rigidbody2D>();
        moveDirection =enemybody.transform.position - playerbody.transform.position;
        Debug.Log("dircion"+moveDirection.normalized);
        HP -= damage;
        float xForce;
        if (moveDirection.x > 0)
        {
             xForce = 1 -moveDirection.normalized.x;
        }
        else
        {
            xForce = -1 -moveDirection.normalized.x;
        }
        Debug.Log("force "+ xForce);
       

        GetComponent<Rigidbody2D>().AddForce(new Vector3(xForce*KnockBackFoce, 5, 0),ForceMode2D.Impulse);
        
        if(HP <= 0)
        {
            Destroy(gameObject);

        }

    }
}
