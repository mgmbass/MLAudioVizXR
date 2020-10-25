using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound()
    {
        if (!audioSource.isActiveAndEnabled)
            audioSource.enabled = true;
       
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void stopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
