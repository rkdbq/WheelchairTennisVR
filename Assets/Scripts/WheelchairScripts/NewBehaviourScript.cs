using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairControllerFromKeyInput : MonoBehaviour
{
    public float maxTorque = 400f;
    public float sound_threshold = 1f;

    public WheelCollider leftWheel;
    public WheelCollider rightWheel;

    Rigidbody rb;
    AudioSource wheelAudio;

    Transform wheelchairTransform;
    string wheelchairDirection = "";
        
    float moveTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wheelchairTransform = GetComponent<Transform>();
        wheelAudio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        moveTime += Time.deltaTime;

        if (moveTime >= 0.1f)
        {
            moveTime = 0f;

            if (Input.GetKey(KeyCode.U))
            {
                wheelchairDirection = "Forward";
            }
            else if (Input.GetKey(KeyCode.K))
            {
                wheelchairDirection = "Right";
            }
            else if (Input.GetKey(KeyCode.H))
            {
                wheelchairDirection = "Left";
            }    
            else if (Input.GetKey(KeyCode.J))
            {
                wheelchairDirection = "Backward";
            }
            else
            {
                wheelchairDirection = "Stop";
            }
        }

        if (wheelchairDirection == "Forward")
        {
            // wheelchairTransform.transform.localPosition += new Vector3(0f, 0f, 0.1f);
            wheelAudio.volume = 0.1f;
            leftWheel.motorTorque = maxTorque;
            rightWheel.motorTorque = maxTorque;
        }
        else if (wheelchairDirection == "Right")
        {
            Debug.Log("right");
            var befLocalRotation = wheelchairTransform.transform.localRotation.eulerAngles;
            var curLocalRotation = befLocalRotation + new Vector3(0f, 2f, 0f);
            wheelchairTransform.transform.localRotation = Quaternion.Euler(curLocalRotation);
            wheelAudio.volume = 0.1f;
            // leftWheel.motorTorque = maxTorque;
            // rightWheel.motorTorque = -maxTorque;
        }
        else if (wheelchairDirection == "Left")
        {
            var befLocalRotation = wheelchairTransform.transform.localRotation.eulerAngles;
            var curLocalRotation = befLocalRotation + new Vector3(0f, -2f, 0f);
            wheelchairTransform.transform.localRotation = Quaternion.Euler(curLocalRotation);
            wheelAudio.volume = 0.1f;
            // leftWheel.motorTorque = -maxTorque;
            // rightWheel.motorTorque = maxTorque;
        }
        else if (wheelchairDirection == "Backward")
        {
            // wheelchairTransform.transform.localPosition -= new Vector3(0f, 0f, 0.1f);
            wheelAudio.volume = 0.1f;
            leftWheel.motorTorque = -maxTorque;
            rightWheel.motorTorque = -maxTorque;
        }
        else if (wheelchairDirection == "Stop")
        {
            wheelAudio.volume = 0f;
            leftWheel.motorTorque = 0f;
            rightWheel.motorTorque = 0f;

            leftWheel.rotationSpeed = 0f;
            rightWheel.rotationSpeed = 0f;
        }


        // wheelAudio.volume = (rb.velocity.magnitude < sound_threshold) ? 0f : 0.1f;
    }
}
