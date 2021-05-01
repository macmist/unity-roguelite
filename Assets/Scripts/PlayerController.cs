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

    public float timeBetweenShots = 0.2f;
    private float shotCountDown;

    private Vector2 offsetFromPlayerToMouse;
    private float weaponAngle;

    private static readonly int LEFT_MOUSE_BUTTON = 0;

    void Start()
    {
        worldCamera = Camera.main;
    }

    void Update()
    {
        MovePlayer();
        RotateWeapon();
        ShootIfPossible();
        Animate();
    }

    private void MovePlayer()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        playerRigidBody.velocity = moveInput * moveSpeed;
    
    }

    private void RotateWeapon() {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = worldCamera.WorldToScreenPoint(transform.localPosition);

        offsetFromPlayerToMouse = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        weaponAngle = Mathf.Atan2(offsetFromPlayerToMouse.y, offsetFromPlayerToMouse.x) * Mathf.Rad2Deg - 90;
        weaponArm.rotation = Quaternion.Euler(0, 0, weaponAngle);
    }

    private void ShootIfPossible() {
        if (CanShoot())
        {
            Shoot();
        }
        else
            DecrementCountDown();
    }

    private bool CanShoot() {
        return LeftButtonClicked() && EnoughTimeHasPassedToShoot();
    }

    private bool LeftButtonClicked() {
        return Input.GetMouseButton(LEFT_MOUSE_BUTTON);
    }

    private bool EnoughTimeHasPassedToShoot() {
        return shotCountDown <= 0;
    }

    private void Shoot() {
        InstantiateBullet();
        ResetCountDown();
    }

    private void InstantiateBullet() {
        Instantiate(bulletToFire, firePoint.position, Quaternion.Euler(0, 0, weaponAngle + 90));
    }

    private void ResetCountDown() {
        shotCountDown = timeBetweenShots;
    }

    private void DecrementCountDown() {
        shotCountDown -= Time.deltaTime;
    }


    private void Animate() {
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
        animator.SetFloat("Horizontal", offsetFromPlayerToMouse.x);
        animator.SetFloat("Vertical", offsetFromPlayerToMouse.y);
    }
}
