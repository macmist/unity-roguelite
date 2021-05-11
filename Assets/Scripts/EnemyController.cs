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

    public int health = 150;

    public GameObject[] deathSplatters;

    public GameObject damageEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,
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

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Kill();
        else
            Instantiate(damageEffect, transform.position, transform.rotation);
    }

    public void Kill() {
        Destroy(gameObject);
        int selectedSplatter = Random.Range(0, deathSplatters.Length);
        int rotation = Random.Range(0, 360);
        Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0, 0, rotation));
    }
}
