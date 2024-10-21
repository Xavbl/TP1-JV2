using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [SerializeField, Min(1)] private int maxHealthPoints = 100;
    [SerializeField, Min(1)] public static float moveSpeed;
    [SerializeField] public Transform[] positions;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField, Tooltip("In Seconds."), Min(1)] private float delayBeforeMoving = 5f;
    [SerializeField, Tooltip("In Seconds."), Min(1)] private float delayBeforeShooting = 8f;

    private AudioSource audioSource;
    private Rigidbody rb;
    private GameObject player;
    private ObjectPool missileObjectPool;
    private Awaitable movingRoutine;
    private Awaitable shootingRoutine;
    private int currentHealthPoints;

    public Vector3 Velocity => rb.linearVelocity;


    private void Awake()
    {
        currentHealthPoints = maxHealthPoints;
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        missileObjectPool = GetComponent<ObjectPool>();
        rb.maxLinearVelocity = moveSpeed;
        this.enabled = false;
    }

    public void SpawnBoss()
    {
        this.enabled = true;
        OnEnable();
    }

    private void OnEnable()
    {
        movingRoutine = ChangingPositionsRoutine();
        shootingRoutine = ShootingMissilesRoutine();
    }

    private void OnDisable()
    {
        movingRoutine.Cancel();
        shootingRoutine.Cancel();
    }

    private async Awaitable ShootingMissilesRoutine()
    {
        while (isActiveAndEnabled)
        {
            var position = this.transform.position;
            GameObject missile = missileObjectPool.Get();
            missile.transform.position = position;
            missile.transform.LookAt(player.transform);
            missile.SetActive(true);

            await Awaitable.WaitForSecondsAsync(delayBeforeShooting);
        }
    }


    private async Awaitable ChangingPositionsRoutine()
    {
        while (isActiveAndEnabled)
        {
            var position = GetNewPosition();
            rb.MovePosition(position);
            this.transform.LookAt(player.transform);

            await Awaitable.WaitForSecondsAsync(delayBeforeMoving);
        }
    }

    private Vector3 GetNewPosition()
    {
        while (true)
        {
            return positions[Random.Range(0, positions.Length)].position;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Bullet>() is not null)
        {
            currentHealthPoints -= Bullet.damage;
            if (currentHealthPoints <= 0)
            {
                enabled = false;
                EventChannels.OnBossDeath();
            }
        }
        return;
    }
}
