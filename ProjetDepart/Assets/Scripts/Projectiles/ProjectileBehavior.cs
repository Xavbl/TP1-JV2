using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10;
    Rigidbody bulletBody;
    void Awake()
    {
        bulletBody = GetComponentInChildren<Rigidbody>();
    }
    void Update()
    {
        bulletBody.linearVelocity = (bulletSpeed * Time.deltaTime * transform.forward);

    }
}
