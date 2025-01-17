using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TennisRacketController : MonoBehaviour
{
    public Transform handTransform;
    Vector3 offset = new Vector3(0f, 0f, 1f);

    void Start()
    {

    }
    void Update()
    {
        SetPosition();
    }

    void SetPosition()
    {
        this.transform.position = handTransform.position + offset;
        this.transform.rotation = handTransform.rotation;
    }
}
