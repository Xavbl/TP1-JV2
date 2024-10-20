using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Collectibles
    private ObjectPool bulletPool;
    private ObjectPool missilePool;

    //Pickups
    private ObjectPool AmmoPickupsPool;
    private ObjectPool HealthPickupsPool;
    private ObjectPool MissilePickupsPool;
    [SerializeField] int HealthPickupHeal = 20;
    [SerializeField] int MissilesPickupAmount = 5;

    [SerializeField] float bulletDuration = 4;
    [SerializeField] int health = 100;
    [SerializeField] int missiles = 0;
    

    private void Awake()
    {
        bulletPool = Finder.BulletObjectPool;
        missilePool = Finder.MissileObjectPool;
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
        Debug.Log("Missiles: "+ missiles);
    }
    public int GetMissiles() { return missiles; }

}
