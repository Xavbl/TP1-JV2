using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField, Min(1)] private int healthPoints = 10;

    private int currentHealthPoints;

    private void Awake()
    {
        currentHealthPoints = healthPoints;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.transform.GetComponent<Bullet>() is not null)
        //{
        //    return;
        //}
        currentHealthPoints--;
    }

    private void Update()
    {
        if (currentHealthPoints <= 0)
        {
            enabled = false;
        }
    }
}
