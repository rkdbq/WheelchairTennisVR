using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Vector3 initDirection;
    AudioSource[] ball_audios;

    AudioSource ball_bounce, ball_hit;

    GameObject resultObject;
    TMPro.TextMeshProUGUI resultTMP;

    bool racket_hit = false;

    void Start()
    {
        resultObject = GameObject.Find("Result");
        resultTMP = resultObject.GetComponent<TMPro.TextMeshProUGUI>();

        ball_audios = GetComponents<AudioSource>();
        ball_bounce = ball_audios[0];
        ball_hit = ball_audios[1];

        Destroy(gameObject, 10f);
    }

    void Update()
    {
        
    }

    public void SetInitDirection(Vector3 direction)
    {
        initDirection = direction;
    }

    public Vector3 GetInitDirection()
    {
        return initDirection;
    }

    public void SetRacketHit(bool is_hit)
    {
        racket_hit = is_hit;
    }

    public bool GetRacketHit()
    {
        return racket_hit;
    }

    public void PlayBallHitAudio() {
        ball_hit.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "out court" || collision.gameObject.tag == "back wall")
        {
            //Debug.Log("floor collision");
            ball_bounce.Play();

            if (racket_hit)
            {
                Debug.Log(collision.gameObject.tag);
                racket_hit = false;
                resultTMP.text = "OUT";
                resultTMP.color = Color.red;

                StartCoroutine(WaitAndExecute(2f));
            }
            else if(collision.gameObject.tag == "back wall")
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "in court" && racket_hit)
        {
            Debug.Log(other.gameObject.tag);
            racket_hit = false;
            resultTMP.text = "IN";
            resultTMP.color = Color.green;

            StartCoroutine(WaitAndExecute(2f));
    }
    }

    IEnumerator WaitAndExecute(float waitTime)
    {
        // waitTime 초 동안 일시 정지
        yield return new WaitForSeconds(waitTime);

        // 일시 정지 후 실행할 코드
        AfterWaitFunction();
    }

    void AfterWaitFunction()
    {
        resultTMP.text = "";
        Destroy(gameObject);
    }
}
