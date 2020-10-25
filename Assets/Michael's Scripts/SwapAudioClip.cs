using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapAudioClip : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setClip1()
    {
        if (audioSource.clip != clip1)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            audioSource.clip = clip1;
            audioSource.Play();
        }
    }

    public void setClip2()
    {
        if (audioSource.clip != clip2)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            audioSource.clip = clip2;
            audioSource.Play();
        }
    }
}
