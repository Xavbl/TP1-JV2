using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] int lifePoints = 100;
    [SerializeField] float fireDelay = 1.0f;
    bool canShoot = true;

    [SerializeField] int speed;
    [SerializeField] float rotationSpeed = 20;

    [SerializeField] float jumpSpeed = 50.5f;
    [SerializeField] float gravityMultiplier = 5;

    [SerializeField] float fireRateBoostDuration = 10f;

    float verticalVelocity = 0f;


    [SerializeField] InputActionReference moveActionReference;
    [SerializeField] InputActionReference shootActionReference;
    [SerializeField] InputActionReference jumpActionRefernce;

    [SerializeField] GameObject cannon;

    CharacterController playerController;
    GameManager gameManager;

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
        gameManager = Finder.GameManager;
    }

    void Update()
    {
        Vector3 playerMovement = CalculateMovement() + Jump();
        playerController.Move(playerMovement);

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float horizontalRotation = mouseDelta.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, horizontalRotation, 0);

        if (shootActionReference.action.triggered && canShoot)
        {
            StartCoroutine(Fire());
        }
    }
    IEnumerator Fire()
    {
        canShoot = false;
        StartCoroutine(gameManager.HandleBullet(cannon.transform.position, gameObject.transform.rotation) );
        yield return new WaitForSeconds(fireDelay);
        canShoot = true;
    }
    Vector3 CalculateMovement()
    {
        var moveVector = Vector3.zero;
        var moveInput = moveActionReference.action.ReadValue<Vector2>();
        Vector3 moveDirectionForward = transform.forward * moveInput.y;
        Vector3 moveDirectionSide = transform.right * moveInput.x;
        Vector3 direction = (moveDirectionForward + moveDirectionSide).normalized;
        Vector3 distance = direction * speed * Time.deltaTime;
        distance += Physics.gravity * gravityMultiplier;
        return distance;
    }
    Vector3 Jump()
    {
        if (playerController.isGrounded)
        {
            if (jumpActionRefernce.action.triggered)
            {
                verticalVelocity = jumpSpeed;
            }
            else
            {
                verticalVelocity = 0f;
            }
        }
        else
        {
            verticalVelocity -= gravityMultiplier * Time.deltaTime;
        }
        return transform.up * verticalVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            gameManager.AddHealth();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Ammo"))
        {
            StartCoroutine(FireRateBoost());
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Missile"))
        {
            gameManager.AddMissiles();
            other.gameObject.SetActive(false);
        }
    }

    void GainLife(int amount)
    {
        lifePoints += amount;
        Debug.Log("Life Points: " + lifePoints);
    }

    IEnumerator FireRateBoost()
    {
            float originalFireDelay = fireDelay;
            fireDelay /= 2;

            yield return new WaitForSeconds(fireRateBoostDuration);

            fireDelay = originalFireDelay;
    }

}
