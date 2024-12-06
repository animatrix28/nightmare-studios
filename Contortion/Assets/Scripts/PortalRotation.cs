using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PortalRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;  // Degrees per second

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}