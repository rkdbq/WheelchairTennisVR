using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandleController : MonoBehaviour
{
    public Transform physicalControllerTransform;

    XRGrabInteractable grabInteractable;
    bool isGrabbed = false;

    Vector3 prevPosition = Vector3.zero;
    Vector3 curPosition;

    Vector3 velocity, localVelocity, controllerVelocity;

    public Transform target; //fix position
    public Rigidbody targetRb;
    Vector3 offset;

    void Start()
    {
        offset = transform.localPosition - target.localPosition;

        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    void FixedUpdate()
    {
        // curPosition = physicalControllerTransform.localPosition;
        // velocity = (curPosition - prevPosition) / Time.deltaTime;

        // localVelocity = velocity - targetRb.velocity;

        //Debug.Log(localVelocity);

        prevPosition = curPosition;

        if (IsGrabbed())
        {
            // Debug.Log("isGrabbed");

            curPosition = physicalControllerTransform.localPosition;
            velocity = (curPosition - prevPosition) / Time.deltaTime;

            localVelocity = velocity - targetRb.velocity;

            // Debug.Log(localVelocity.magnitude);
            //Debug.Log(localVelocity.sqrMagnitude);
            //Debug.Log(velocity);
            //Debug.Log(targetRb.velocity);

        }
        else
        {

            localVelocity = Vector3.zero;
            //Debug.Log("isNotGrabbed");
            //Debug.Log(localVelocity.sqrMagnitude);
            //Debug.Log(targetRb.velocity);

            //Vector3 rotatedOffset = target.localRotation * offset;
            //transform.localPosition = target.localPosition + rotatedOffset;

            //transform.rotation = target.rotation;
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }

    public bool IsGrabbed()
    {
        return isGrabbed;
    }

    public float GetLocalSpeed()
    {
        return localVelocity.magnitude;
    }

    public float GetZDirection()
    {
        if(!IsGrabbed() || GetLocalSpeed() < 1.5f ) return 0f;

        return localVelocity.z > 0f ? 1f : -1f;
    }

    void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.name == "Right Controller" || collision.gameObject.name == "Left Controller") {
            XRDirectInteractor directInteractor = collision.gameObject.GetComponent<XRDirectInteractor>();
            directInteractor.SendHapticImpulse(0.3f, 0.5f);
            Debug.Log(collision.gameObject.name);
        }
    }
}
