using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAccelator : MonoBehaviour
{
    public float maxTorque;
    public HandleController leftHandleController;
    public HandleController rightHandleController;

    WheelCollider wheel;


    void Start()
    {
        wheel = GetComponent<WheelCollider>();
    }

    void FixedUpdate()
    {
        //Debug.Log(handleController.GetLocalSpeed());
        //Debug.Log(handleController.GetZDirection());

        // float motor = Math.Min(maxTorque, handleController.GetLocalSpeed() * 150f) * handleController.GetZDirection();
        float motor = maxTorque * leftHandleController.GetZDirection();

        if (leftHandleController.GetZDirection() > 0 && rightHandleController.GetZDirection() > 0) Debug.Log("Forward");
        if (leftHandleController.GetZDirection() > 0 && rightHandleController.GetZDirection() <= 0) Debug.Log("Right");
        if (leftHandleController.GetZDirection() >= 0 && rightHandleController.GetZDirection() < 0) Debug.Log("Right");
        if (leftHandleController.GetZDirection() <= 0 && rightHandleController.GetZDirection() > 0) Debug.Log("Left");
        if (leftHandleController.GetZDirection() < 0 && rightHandleController.GetZDirection() >= 0) Debug.Log("Left");
        if (leftHandleController.GetZDirection() < 0 && rightHandleController.GetZDirection() < 0) Debug.Log("Backward");

        Debug.Log(motor);

        if (Math.Abs(motor) < 5f)
        {
            wheel.motorTorque = 0f;
            wheel.rotationSpeed = 0f;
        }
        else
        {
            wheel.motorTorque = motor;
        }
    }
}