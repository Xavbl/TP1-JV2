using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float duration = 15;

    Rigidbody boxBody;
    void Awake()
    {
        StartCoroutine(DurationCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,rotationSpeed*Time.deltaTime, 0);


        if (duration < 0)
        {
            gameObject.SetActive(false);  
        }
    }
    private IEnumerator DurationCountdown()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

}
