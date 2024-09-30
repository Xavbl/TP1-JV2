using UnityEngine;
using UnityEngine.UI;

public class AlienSpawnManager : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject alienPrefab;
    [SerializeField, Min(1)] private int maxNbEnemies = 20;
    [SerializeField, Tooltip("In Seconds."), Min(1)] private float delay = 5f;

    [Header("Spawnpoints")]
    [SerializeField] private Transform[] portals;

    private Awaitable routine;
    private ObjectPool alienObjectPool;
    private int nbEnemies;

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
            // Code pour la position des portaux obtenu grâce à ce lien
            // https://stackoverflow.com/questions/75304310/creating-multiple-spawnpoints-in-unity-multiplayer

            Debug.Log(nbEnemies);

            var position = getPortal().position;
            GameObject alien = alienObjectPool.GetInactiveChild();
            alien.transform.position = position;
            alien.transform.rotation = Quaternion.identity;
            alien.SetActive(true);
            await Awaitable.WaitForSecondsAsync(delay);

            nbEnemies++;
        }
    }

    private Transform getPortal()
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
}
