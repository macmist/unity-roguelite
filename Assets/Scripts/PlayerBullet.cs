using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public float speed = 7.5f;
    public Rigidbody2D bulletRigidBody;

    public GameObject impactEffect;


    void Update()
    {
        bulletRigidBody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ShowParticleAndDestroy();

    }

    private void OnBecameInvisible()
    {
        DestroyBullet();
    }

    private void ShowParticleAndDestroy() {
        Instantiate(impactEffect, transform.position, transform.rotation);
        DestroyBullet();
    }

    private void DestroyBullet() {
        Destroy(gameObject);
    }
}
