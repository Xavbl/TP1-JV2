using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] int jumpSpeed = 10;
    [SerializeField] float jumpMaxDuration = 10;
    [SerializeField] float gravityMultiplier = 1;
    float jumpTimer;
    bool isJumping = false;


    [SerializeField] InputActionReference moveActionReference;
    [SerializeField] InputActionReference shootActionReference;
    [SerializeField] InputActionReference jumpActionRefernce;

    [SerializeField] GameObject cannon;

    CharacterController playerController;
    private ObjectPool bulletObjectPool;

    private void Awake()
    {
        bulletObjectPool = Finder.BulletObjectPool;
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerMovement = CalculateMovement() + Jump();
        playerController.Move(playerMovement);

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float horizontalRotation = mouseDelta.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, horizontalRotation, 0);

        if (shootActionReference.action.triggered)
        {
            GameObject bullet = bulletObjectPool.Get();
            if (bullet != null)
            {
                bullet.transform.position = cannon.transform.position;
                bullet.transform.rotation = transform.rotation;
            }
        }
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
        if(playerController.isGrounded && jumpActionRefernce.action.triggered)
        {
            isJumping = true;
            jumpTimer = jumpMaxDuration;
        }
        Debug.Log(isJumping);
        if (jumpActionRefernce.action.WasCompletedThisFrame() || jumpTimer <= 0)
        {
            isJumping = false;
        }

        Vector3 jumpVector = Vector3.zero;
        if (isJumping)
        {
            jumpVector = transform.up * jumpSpeed *Time.deltaTime;
            jumpTimer -= Time.deltaTime;    
        }
        return jumpVector;
    }

}
