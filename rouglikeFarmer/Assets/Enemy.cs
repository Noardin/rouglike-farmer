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
        moveDirection = playerbody.transform.position - enemybody.transform.position;
        HP -= damage;


        enemybody.AddForce(moveDirection.normalized * -200f*damage);
        if(HP <= 0)
        {
            Destroy(gameObject);

        }

    }
}
