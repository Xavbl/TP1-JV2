using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10;
    [SerializeField] public static int damage = 10;
    Rigidbody bulletBody;
    void Awake()
    {
        bulletBody = GetComponentInChildren<Rigidbody>();
    }

    private void OnEnable()
    {
        bulletBody.linearVelocity = Vector3.zero;
    }

    void Update()
    {
        bulletBody.linearVelocity = (bulletSpeed * Time.deltaTime * transform.forward);
    }
}
