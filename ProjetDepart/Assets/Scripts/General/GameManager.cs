using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Collectibles
    private ObjectPool bulletPool;
    private ObjectPool missilePool;

    //Pickups
    private ObjectPool ammoPickupsPool;
    private ObjectPool healthPickupsPool;
    private ObjectPool missilePickupsPool;
    [SerializeField] int HealthPickupHeal = 20;
    [SerializeField] int MissilesPickupAmount = 5;
    [SerializeField] private float pickupDropChance = 0.5f;
    [SerializeField] int collectibleDuration = 15;

    [SerializeField] float bulletDuration = 4;
    [SerializeField] int health = 100;
    [SerializeField] int missiles = 0;


    private void Awake()
    {
        bulletPool = Finder.BulletObjectPool;
        missilePool = Finder.MissileObjectPool;

        ammoPickupsPool = Finder.AmmoBoxPool;
        healthPickupsPool = Finder.HealthBoxPool;
        missilePickupsPool = Finder.MissileBoxPool;

    }

    public IEnumerator HandleBullet(Vector3 pos, Quaternion angle)
    {
        GameObject bullet;
        if (missiles > 0)
        {
            bullet = missilePool.Get();
            missiles--;
        }
        else
        {
            bullet = bulletPool.Get();

        }
        if (bullet != null)
        {
            bullet.transform.position = pos;
            bullet.transform.rotation = angle;
        }
        yield return new WaitForSeconds(bulletDuration);
        bulletPool.Release(bullet);
    }

    public void AddHealth()
    {
        health += HealthPickupHeal;
        Debug.Log("Health: " + health);
    }
    public int GetHealth() { return health; }

    public void AddMissiles()
    {
        missiles += MissilesPickupAmount;
        Debug.Log("Missiles: " + missiles);
    }
    public int GetMissiles() { return missiles; }

    private GameObject getCollectibleBox()
    {
        int randomPick = Random.Range(0, 3);

        GameObject collectibleBox = null;
        switch (randomPick)
        {
            case 0:
                collectibleBox = ammoPickupsPool.Get();
                StartCoroutine(ReleaseCollectibleBox(collectibleBox, ammoPickupsPool));
                break;
            case 1:
                collectibleBox = healthPickupsPool.Get();
                StartCoroutine(ReleaseCollectibleBox(collectibleBox, healthPickupsPool));

                break;
            case 2:
                collectibleBox = missilePickupsPool.Get();
                StartCoroutine(ReleaseCollectibleBox(collectibleBox, missilePickupsPool));

                break;
        }

        return collectibleBox;
    }

    public void HandleAlienDeath(Vector3 position)
    {
        if (Random.value <= pickupDropChance)
        {
            GameObject collectibleBox = getCollectibleBox();

            if (collectibleBox != null)
            {
                collectibleBox.SetActive(true);
                collectibleBox.transform.position = position;
            }
        }
    }
    private IEnumerator ReleaseCollectibleBox(GameObject collectibleBox, ObjectPool pool)
    {
        yield return new WaitForSeconds(collectibleDuration);
        pool.Release(collectibleBox);
    }
}

