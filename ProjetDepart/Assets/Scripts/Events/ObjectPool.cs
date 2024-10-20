using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField, Min(1)] private int objectCount = 20;
    private void Awake()
    {
        for (int i = 0; i < objectCount; i++)
        {
            Debug.Log("(Awake) Now at " + i);
            var instance = Instantiate(prefab, transform);
            instance.SetActive(false);
        }
    }

    public GameObject Get()
    {
        for (int i = 0; i < objectCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (!child.activeSelf)
            {
                child.transform.parent = null;
                child.SetActive(true);
                Debug.Log("Child at " + child.transform.position.ToString());
                return child;
            }
        }
        return null;
    }

    public void Release(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = transform;
    }
}