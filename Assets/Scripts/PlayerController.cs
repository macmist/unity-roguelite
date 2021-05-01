using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D playerRigidBody;
    public Transform weaponArm;

    public Animator animator;

    private Camera worldCamera;

    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots = 0.1f;
    private float shotCountDown;

    void Start()
    {
        worldCamera = Camera.main;
    }

    void Update()
    {
        // move player around
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        playerRigidBody.velocity = moveInput * moveSpeed;

        animator.SetFloat("Speed", moveInput.sqrMagnitude);


        // rotate weapon section
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = worldCamera.WorldToScreenPoint(transform.localPosition);


        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);

        animator.SetFloat("Horizontal", offset.x);
        animator.SetFloat("Vertical", offset.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg - 90;

        weaponArm.rotation = Quaternion.Euler(0, 0, angle);


        // shoot
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(bulletToFire, firePoint.position, Quaternion.Euler(0, 0, angle + 90));
            shotCountDown = timeBetweenShots;
        }

        if (Input.GetMouseButton(0)) {
            shotCountDown -= Time.deltaTime;
            if (shotCountDown <= 0) {
                Instantiate(bulletToFire, firePoint.position, Quaternion.Euler(0, 0, angle + 90));
                shotCountDown = timeBetweenShots;
            }
        }
    }
}
