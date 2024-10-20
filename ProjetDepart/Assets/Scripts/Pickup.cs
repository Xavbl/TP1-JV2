using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10;

    Rigidbody boxBody;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,rotationSpeed*Time.deltaTime, 0);
    }
}
