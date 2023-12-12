using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.pitch = Random.Range(1f, 1.2f);
        audio.volume *= Random.Range(0.9f, 1f);
        audio.Play();
    }
    private void FixedUpdate()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
