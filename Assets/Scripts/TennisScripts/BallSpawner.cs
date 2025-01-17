using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    int[] angles = { 20, 30, 45 };
    float[ , ] speeds = { { 15f, 18f }, { 12f, 16f }, { 11f, 15f } };

    float angle_y = 0f;
    float angle_z = 30f;
    float speed = 15f;

    public Transform target;
    public GameObject ballPrefab;

    float timeAfterSpawn = 0f;

    void Start()
    {
        Random.InitState((int)Time.time);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeAfterSpawn += Time.deltaTime;
        if(timeAfterSpawn >= 5f)
        {
            timeAfterSpawn = 0f;


            angle_y = Random.Range(-10f, 10f);
            int angleIdx = Random.Range(0, 3);
            angle_z = angles[angleIdx];
            speed = Random.Range(speeds[angleIdx, 0], speeds[angleIdx, 1]);

            // TEMP for competition
            angle_y = 0f;
            angle_z = 45f;
            speed = 13.5f;

            transform.rotation = Quaternion.Euler(0, angle_y, angle_z);

            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f));

            Rigidbody ballRb = ball.GetComponent<Rigidbody>();
            //Debug.Log(target.position);
            //ball.transform.LookAt(target);

            ballRb.velocity = this.transform.right * speed;

            BallController ballController = ball.GetComponent<BallController>();
            ballController.SetInitDirection(ballRb.velocity.normalized);
        }
    }
}
