using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Enemy: MonoBehaviour
{
    public int HP = 100;
    private GameObject player;
    private Rigidbody2D playerbody;
    private Vector3 moveDirection;
    public Rigidbody2D enemybody;
    public float KnockBackFoce = 10f;
    public LayerMask whatisPlayer;
    public GameObject particleSystem;
    public Color HurtParticleColor;

    protected virtual void Awake()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }
    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        GameObject instanceParticle = Instantiate(particleSystem,transform.position,Quaternion.identity);
        ParticleSystem.MainModule mainModule = instanceParticle.GetComponent<ParticleSystem>().main;
        mainModule.startColor = HurtParticleColor;
//        Debug.Log("player", player);
//        playerbody = player.GetComponent<Rigidbody2D>();
//        moveDirection = enemybody.transform.position - playerbody.transform.position;
//        Debug.Log("dircion"+moveDirection.normalized);
        HP -= damage;
//        float xForce;
//        if (moveDirection.x > 0)
//        {
//             xForce = 1 -moveDirection.normalized.x;
//        }
//        else
//        {
//            xForce = -1 -moveDirection.normalized.x;
//        }
//        Debug.Log("force "+ xForce);
//       
//
//        GetComponent<Rigidbody2D>().AddForce(new Vector3(xForce*KnockBackFoce, 5, 0),ForceMode2D.Impulse);
        
        if(HP <= 0)
        {
            Destroy(gameObject);

        }

    }
}
