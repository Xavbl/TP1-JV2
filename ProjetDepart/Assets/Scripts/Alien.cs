using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    private new CharacterController characterController;
    private Vector3 move;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (characterController.isGrounded is false)
        {
            move += Physics.gravity;
        }

    }
}
