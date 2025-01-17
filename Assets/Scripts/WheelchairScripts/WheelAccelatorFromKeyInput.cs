using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAccelatorFromKeyInput : MonoBehaviour
{
    public float maxTorque;

    WheelCollider wheel;

    void Start()
    {
        wheel = GetComponent<WheelCollider>();
    }

    void Update()
    {
        //float motor = maxTorque * Input.GetAxis("Vertical");
        float motor = 0;

        if(Input.GetKey(KeyCode.I))
        {
            motor = maxTorque;
        }


        if (motor == 0f)
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