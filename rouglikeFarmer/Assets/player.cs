using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Health_Manager healthManager;
    public Transform position;
    private Vector3 GoToPosition;
    private float GoToSpeed;
    private bool movesTowards;
    private Animator _animator;
    public CameraFollow cameraController;
    public bool PlayerControlledMovementDisabled = true;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        healthManager = GetComponentInChildren<Health_Manager>();
        position = transform;
    }
    public void TakeDamage(double damage)
    {
        healthManager.TakeDamage(damage);
    }

    public void GoTo(Vector2 GoToPosition, float speed)
    {
        this.GoToPosition = GoToPosition;
        GoToSpeed = speed;
        movesTowards = true;
        _animator.SetFloat("Speed",1f);
        _animator.SetBool("Grounded", true);
        PlayerControlledMovementDisabled = true;


    }

    private void Update()
    {
        if (movesTowards)
        {
            float step = GoToSpeed * Time.deltaTime;
            GoToPosition.y = transform.position.y;
            position.position = Vector2.MoveTowards(position.position, GoToPosition, step);
            if (position.position == GoToPosition)
            {
                movesTowards = false;
                PlayerControlledMovementDisabled = false;
                _animator.SetFloat("Speed",0f);
                cameraController.Follow(transform);
            }
            
        }
    }
}
