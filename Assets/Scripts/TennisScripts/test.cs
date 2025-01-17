using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Vector3 ballDirection = other.GetComponent<BallController>().GetInitDirection();

        ballDirection.x *= -1f;
        ballDirection.z *= -1f;
        ballDirection.Normalize();


        other.GetComponent<Rigidbody>().velocity = AdjustVectorToAngle(ballDirection, 30) * 30f;
    }

    Vector3 AdjustVectorToAngle(Vector3 vector, float targetAngleDegrees)
    {
        // 목표 각도를 라디안으로 변환
        float targetAngleRadians = targetAngleDegrees * Mathf.Deg2Rad;

        // xz 평면의 크기
        float xzMagnitude = new Vector2(vector.x, vector.z).magnitude;

        // y 성분의 크기를 조정하여 목표 각도를 만족
        float yMagnitude = xzMagnitude * Mathf.Tan(targetAngleRadians);

        // 새 벡터 생성
        Vector3 adjustedVector = new Vector3(vector.x, yMagnitude, vector.z);

        return adjustedVector;
    }
}
