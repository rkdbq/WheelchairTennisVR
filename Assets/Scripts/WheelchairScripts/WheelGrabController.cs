using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelGrabableController : MonoBehaviour
{
    public Transform wheelPosition;

    Vector3 offset;

    Rigidbody rb;

    private void Start()
    {
        offset = this.transform.localPosition - wheelPosition.localPosition;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 rotatedOffset = wheelPosition.localRotation * offset;
        transform.localPosition = wheelPosition.localPosition + rotatedOffset;

        this.transform.rotation = wheelPosition.rotation;

        //Debug.Log(rb.velocity);
    }
}
