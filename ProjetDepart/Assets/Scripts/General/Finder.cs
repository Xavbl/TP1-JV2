using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class Finder
{
    private static ObjectPool bulletObjectPool;
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
}
