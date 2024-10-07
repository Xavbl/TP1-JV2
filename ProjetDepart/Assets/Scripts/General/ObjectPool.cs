using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField, Min(1)] private int objectCount = 10;

    private void Awake()
    {
        for (var i = 0; i < objectCount; i++)
        {
            var instance = Instantiate(prefab, transform);
            instance.SetActive(false);
        }
    }
    public GameObject Get()
    {
        for (var i = 0; i < objectCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (!child.activeInHierarchy)
            {
                child.transform.parent = null;
                child.SetActive(true);
                return child;
            }
        }
        return null;
    }

    public void Release(GameObject instance)
    {
        instance.SetActive(false);
        instance.transform.parent = transform;
    }
}
