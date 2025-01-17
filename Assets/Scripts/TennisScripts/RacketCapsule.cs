using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketCapsule : MonoBehaviour
{
    [SerializeField]
    RacketCapsuleFollower racketCapsuleFollowerPrefab;

    public float sensitivity;

    void SpawnRacketCapsuleFollower()
    {
        var follower = Instantiate(racketCapsuleFollowerPrefab);
        follower.transform.position = transform.position;
        follower.gameObject.tag = "racket";

        var followerMeshRenderer = follower.GetComponent<MeshRenderer>();
        followerMeshRenderer.enabled = false;

        follower.SetSensitivity(sensitivity);

        follower.SetFollowTarget(this);
    }

    void Start()
    {
        SpawnRacketCapsuleFollower();
    }
}
