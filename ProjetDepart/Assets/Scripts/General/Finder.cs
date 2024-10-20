using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class Finder
{
    private static ObjectPool bulletObjectPool;
    private static ObjectPool missileObjectPool;

    public static ObjectPool BulletObjectPool
    {
        get
        {
            if (bulletObjectPool is null)
            {
               bulletObjectPool = GameObject.Find("BulletObjectPool").GetComponent<ObjectPool>();
                Debug.Log(bulletObjectPool.transform.position);
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
                Debug.Log(missileObjectPool.transform.position);
            }
            return missileObjectPool;
        }
    }
}
