using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RacketCapsuleFollower : MonoBehaviour
{
    [Range(0, 1)]
    public float hapticIntencity = 1f;
    public float hapticDuration = 0.1f;

    public float defaultSpeed = 8f;
    public float fromRacketSpeed = 1.5f;

    GameObject cameraObject;
    PlayerController playerController;

    GameObject controllerObject;
    XRDirectInteractor directInteractor;

    RacketCapsule racketCapsule;
    Rigidbody rb;
    Vector3 velocity;

    float velocityRate = 2f;

    float sensitivity = 100f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        controllerObject = GameObject.Find("Right Controller");
        directInteractor = controllerObject.GetComponent<XRDirectInteractor>();

        cameraObject = GameObject.Find("Main Camera");
        playerController = cameraObject.GetComponent<PlayerController>();

        this.transform.localScale = racketCapsule.transform.localScale;
    }

    void FixedUpdate()
    {
        Vector3 destination = racketCapsule.transform.position;
        rb.transform.rotation = racketCapsule.transform.rotation;

        velocity = (destination - rb.transform.position) * sensitivity;

        rb.velocity = velocity;

        //Debug.Log(rb.velocity.sqrMagnitude);
    }

    public void SetSensitivity(float sensitivity) {
        this.sensitivity = sensitivity;
    }

    public void SetFollowTarget(RacketCapsule racketCapsuleFollower)
    {
        this.racketCapsule = racketCapsuleFollower;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ball" && !other.GetComponent<BallController>().GetRacketHit())
        {
            BallController ballController = other.GetComponent<BallController>();
            ballController.SetRacketHit(true);
            ballController.PlayBallHitAudio();

            Vector3 ballDirection = ballController.GetInitDirection();

            ballDirection.x *= -1f;
            ballDirection.z *= -1f;
            ballDirection.Normalize();

            Vector3 defaultVelocity = AdjustVectorToAngle(ballDirection, 30) * defaultSpeed;
            Vector3 racketVelocity = rb.velocity;
            float sign = (racketVelocity.y >= 0) ? 1.0f : -1.0f;
            racketVelocity.y = sign * Mathf.Sqrt(Mathf.Abs(racketVelocity.y));

            //Debug.Log(defaultVelocity);
            other.GetComponent<Rigidbody>().velocity = defaultVelocity + fromRacketSpeed * racketVelocity;
            //Debug.Log("default: " + defaultVelocity.magnitude);
            //Debug.Log("racket: " + racketVelocity.magnitude);
            //Debug.Log("total: " + other.GetComponent<Rigidbody>().velocity.magnitude);

            playerController.PlayGroan();

            directInteractor.SendHapticImpulse(hapticIntencity, hapticDuration);
            //Debug.Log("haptic done");
        }
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

        return adjustedVector.normalized;
    }
}
