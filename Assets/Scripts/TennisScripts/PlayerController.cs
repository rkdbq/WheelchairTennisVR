using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float groanProb = 0.3f;
    AudioSource[] groans;

    void Start()
    {
        groans = GetComponents<AudioSource>();
    }

    public void PlayGroan()
    {
        Random.InitState((int)Time.time);
        float prob = Random.Range(0f, 1f);
        if (prob > groanProb) return;

        foreach (var groan in groans) {
            if (groan.isPlaying) return;
        }

        Random.InitState((int)Time.time);
        int index = Random.Range(0, 4);
        groans[index].Play();
    }
}
