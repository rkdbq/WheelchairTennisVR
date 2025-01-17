using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform target;
    Vector3 offset;

    void Start()
    {
        offset = transform.localPosition - target.localPosition;
    }

    void FixedUpdate()
    {
        Vector3 rotatedOffset = target.localRotation * offset;
        transform.localPosition = target.localPosition + rotatedOffset;

        transform.rotation = target.rotation;
    }
}
