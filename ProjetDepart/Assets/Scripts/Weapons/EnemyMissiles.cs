using UnityEngine;

public class EnemyMissiles : MonoBehaviour
{
    [SerializeField, Min(1)] private int damage = 5;
    private Rigidbody rb;
    private GameObject player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Player>() is not null)
        {
            EventChannels.OnPlayerHealthChange.Invoke(damage);
        }
    }
}
