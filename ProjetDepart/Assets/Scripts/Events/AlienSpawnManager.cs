using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AlienSpawnManager : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject alienPrefab;
    [SerializeField, Min(1)] public static int maxNbEnemies = 20;
    [SerializeField, Tooltip("In Seconds."), Min(1)] private float delay = 1f;

    [Header("Spawnpoints")]
    [SerializeField] public Transform[] portals;
    private Awaitable routine;
    private ObjectPool alienObjectPool;
    private int nbEnemies = 0;

    private void Awake()
    {
        alienObjectPool = GetComponent<ObjectPool>();
    }

    private void OnEnable()
    {
        routine = SpawningRoutine();
    }

    private void OnDisable()
    {
        routine.Cancel();
    }

    private async Awaitable SpawningRoutine()
    {
        while (isActiveAndEnabled && nbEnemies <= maxNbEnemies)
        {  
            Debug.Log("Number of enemies : " + nbEnemies + ", Max is : " + maxNbEnemies);
            var position = GetPortal().position;
            GameObject alien = alienObjectPool.Get();
            alien.transform.position = position;
            alien.transform.rotation = Quaternion.identity;
            alien.SetActive(true);
            await Awaitable.WaitForSecondsAsync(delay);

            nbEnemies++;
        }
    }

    private Transform GetPortal()
    {
        while (true)
        {
            var portal = portals[Random.Range(0, portals.Length)];
            if (portal.gameObject.activeSelf)
            {
                return portal.transform;
            }
        }
    }

    public int GetLength()
    {
        return portals.Length;
    }
}