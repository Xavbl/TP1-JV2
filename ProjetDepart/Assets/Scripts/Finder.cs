using System.Diagnostics.Tracing;
using UnityEngine;

public class Finder : MonoBehaviour
{
    private static Alien alien;
    private static AlienSpawnManager alienSpawnManager;
    private static Portal portal;
    private static Boss boss;
    private static EventChannels eventChannels;
    private static ObjectPool objectPool;

    public static ObjectPool ObjectPool
    {
        get
        {
            if (objectPool == null)
            {
                objectPool = GameObject.FindWithTag("SpawnManager")?.GetComponent<ObjectPool>();
            }
            return objectPool;
        }
    }

    public static EventChannels EventChannels
    {
        get
        {
            if (eventChannels == null)
            {
                eventChannels = GameObject.FindWithTag("GameController")?.GetComponent<EventChannels>();
            }
            return eventChannels;
        }
    }

    public static AlienSpawnManager AlienSpawnManager
    {
        get
        {
            if(alienSpawnManager == null)
            {
                alienSpawnManager = GameObject.FindWithTag("SpawnManager")?.GetComponent<AlienSpawnManager>();
            }
            return alienSpawnManager;
        }
    }

    public static Alien Alien
    {
        get
        {
            if(alien == null)
            {
                alien = GameObject.FindWithTag("Enemy")?.GetComponent<Alien>();
            }
            return alien;
        }
    }

    public static Portal Portal
    {
        get
        {
            if (portal == null)
            {
                portal = GameObject.FindWithTag("Spawner")?.GetComponent<Portal>();
            }
            return portal;
        }
    }

    public static Boss Boss
    {
        get
        {
            if (boss == null)
            {
                boss = GameObject.FindWithTag("Boss")?.GetComponent<Boss>();
            }
            return boss;
        }
    }
}
