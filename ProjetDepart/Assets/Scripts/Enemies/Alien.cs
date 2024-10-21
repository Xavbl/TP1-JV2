using System;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    [SerializeField] private AudioClip deathSFX;
    private AudioSource audioSource;
    private CharacterController characterController;
    private Rigidbody rb;
    private GameObject player;
    private NavMeshAgent agent;
    private ObjectPool objectPool;
    private EventChannels eventChannels;
    public Vector3 Velocity => agent.velocity;

    private void Awake()
    {
        tag = "Enemy";
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        objectPool = Finder.ObjectPool;
        audioSource = GetComponent<AudioSource>();
        EventChannels.OnAlienDeath += OnAlienDeath;
    }

    private void OnAlienDeath(Alien arg0)
    {
        EventChannels.OnAlienDeath(this);
        audioSource.PlayOneShot(deathSFX);
        objectPool.Release(this.gameObject);
    }

    public void OnEnable()
    {
        agent.enabled = true;
    }

    void FixedUpdate()
    {
        agent.SetDestination(player.transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Bullet>() is not null)
        {
            enabled = false;
            OnAlienDeath(this);
        }
        return;
    }
}
