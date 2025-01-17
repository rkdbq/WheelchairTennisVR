using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityDebugger : MonoBehaviour
{   
    [SerializeField]
    float maxVelocity = 20f;

    void Update()
    {
        this.GetComponent<Renderer>().material.color = ColorForVelocity();
    }

    Color ColorForVelocity()
    {
        float velocity = transform.parent.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        //float velocity = GetComponent<Rigidbody>().velocity.magnitude;

        Debug.Log(velocity);

        return Color.Lerp(Color.green, Color.red, velocity / maxVelocity);
    }
}
