using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject MissileShell;
    [SerializeField] float bulletSpeed = 10;

    Rigidbody bulletBody;
    private bool hasExploded = false;
    void Awake()
    {
        bulletBody = GetComponentInChildren<Rigidbody>();
    }
    private void OnEnable()
    {
        explosionEffect.SetActive(false);
        MissileShell.SetActive(true);
        hasExploded = false;
    }
    void Update()
    {
        if (!hasExploded)
        {
            bulletBody.linearVelocity = (bulletSpeed * Time.deltaTime * transform.forward);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!hasExploded && !other.CompareTag("Player"))
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;
        bulletBody.linearVelocity = Vector3.zero;
        MissileShell.SetActive(false);
        explosionEffect.SetActive(true);

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, explosionRadius, Vector3.up);

        foreach (RaycastHit hit in hits)
        {
            Debug.Log("Object hit by explosion: " + hit.collider.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
