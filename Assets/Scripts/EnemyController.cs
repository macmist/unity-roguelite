using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D enemyBody;

    public float moveSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;


    public Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,
                             PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;

        }
        else
            moveDirection = Vector3.zero;
        moveDirection.Normalize();

        enemyBody.velocity = moveDirection * moveSpeed;
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

    }
}
