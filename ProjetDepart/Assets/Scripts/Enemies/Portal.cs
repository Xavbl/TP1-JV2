using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.GPUSort;

public class Portal : MonoBehaviour
{
    [SerializeField, Min(1)] private int healthPoints = 10;
    public int currentHealthPoints;

    private UnityEvent onPortalHealthChanged;

    private void Awake()
    {
        currentHealthPoints = healthPoints;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Bullet>() is not null)
        {
            currentHealthPoints -= Bullet.damage;
            if (currentHealthPoints <= 0)
            {
                enabled = false;
                this.GetComponent<Renderer>().enabled = false;
                EventChannels.OnPortalDeath.Invoke(this);
                Finder.Tracker.nbPortalsDead++;
            }
            EventChannels.OnPortalHealthChange.Invoke(this, currentHealthPoints);
        }
        return;
    }
}
