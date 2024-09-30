using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] float rotationSpeed = 10;

    Rigidbody playerBody;
    CharacterController playerController;

    [SerializeField] InputActionReference moveActionReference;
    [SerializeField] InputActionReference shootActionReference;
    [SerializeField] InputActionReference jumpActionRefernce;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveVector = Vector3.zero;
        var moveInput = moveActionReference.action.ReadValue<Vector2>();
        Vector3 moveDirectionForward = transform.forward * moveInput.y;
        Vector3 moveDirectionSide = transform.right * moveInput.x;
        Vector3 direction = (moveDirectionForward + moveDirectionSide).normalized;
        Vector3 distance = direction * speed * Time.deltaTime;
        distance += Physics.gravity;

        playerController.Move(distance);

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float horizontalRotation = mouseDelta.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, horizontalRotation, 0);
    }
}
