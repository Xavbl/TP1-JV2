using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class Finder
{
    private static ObjectPool bulletObjectPool;
    private static ObjectPool missileObjectPool;
    private static GameManager gameManager;

    private static ObjectPool ammoBoxPool;
    private static ObjectPool healthBoxPool;
    private static ObjectPool missileBoxPool;
    public static ObjectPool BulletObjectPool
    {
        get
        {
            if (bulletObjectPool is null)
            {
               bulletObjectPool = GameObject.Find("BulletObjectPool").GetComponent<ObjectPool>();
            }
            return bulletObjectPool;
        }
    }

    public static ObjectPool MissileObjectPool
    {
        get
        {
            if (missileObjectPool is null)
            {
                missileObjectPool = GameObject.Find("MissileObjectPool").GetComponent<ObjectPool>();
            }
            return missileObjectPool;
        }
    }
    public static GameManager GameManager
    {
        get
        {
            if (gameManager is null)
            {
                gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            return gameManager;
        }
    }

    public static ObjectPool AmmoBoxPool
    {
        get
        {
            if (ammoBoxPool is null)
            {
                ammoBoxPool = GameObject.Find("AmmoBoxPool").GetComponent<ObjectPool>();
            }
            return ammoBoxPool;
        }
    }

    public static ObjectPool HealthBoxPool
    {
        get
        {
            if (healthBoxPool is null)
            {
                healthBoxPool = GameObject.Find("HealthBoxPool").GetComponent<ObjectPool>();
            }
            return healthBoxPool;
        }
    }
    public static ObjectPool MissileBoxPool
    {
        get
        {
            if (missileBoxPool is null)
            {
                missileBoxPool = GameObject.Find("MissileBoxPool").GetComponent<ObjectPool>();
            }
            return missileBoxPool;
        }
    }
}
