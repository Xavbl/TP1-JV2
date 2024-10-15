using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    [SerializeField, Min(1)] private float moveSpeed;
    private CharacterController characterController;
    private Rigidbody rb;
    private Vector3 move = new Vector3(0,0,0);
    private GameObject player;
    private NavMeshAgent agent;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    public void OnEnable()
    {
        agent.enabled = true;
    }

    void FixedUpdate()
    {
        if (!characterController.isGrounded)
        {
            move += Physics.gravity;
        }
        //move = (player.transform.position - transform.position).normalized;
        //characterController.Move(move * Time.fixedDeltaTime);
        agent.SetDestination(player.transform.position);
    }
}
