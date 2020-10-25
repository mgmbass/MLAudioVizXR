using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Visualize : MonoBehaviour
{
    AudioSource _audioSource;
    AudioHighPassFilter hfFilter;
    AudioLowPassFilter lfFilter;
    public static float[] samples = new float[512];
    public static float[] buffers = new float[512];
    public static float[] decreaseBuffer = new float[512];

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        hfFilter = GetComponent<AudioHighPassFilter>();
        lfFilter = GetComponent<AudioLowPassFilter>();

    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudio();
        HighPassZeros();
        LowPassZeros();
        buffer();
        
    }

    void GetSpectrumAudio()
    {
        _audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
    }

    void HighPassZeros()
    {
        int zeros = (int)hfFilter.cutoffFrequency / 42;
        for(int i = 0; i<zeros; i++)
        {
            samples[i] = 0.0001f;
        }
    }

    void LowPassZeros()
    {
        int zeros = (int)lfFilter.cutoffFrequency / 42;
        for(int i = 511; i>=zeros; i--)
        {
            samples[i] = 0.0001f;
        }
    }
    void buffer()
    {
        for (int i = 0; i < 512; i++)
        {
            if (samples[i] > buffers[i])
            {
               buffers[i] = samples[i];
                decreaseBuffer[i] = 0.000005f;
            }

            if (samples[i] < buffers[i])
            {
                buffers[i] -= decreaseBuffer[i];
                decreaseBuffer[i] *= 1.2f;
            }
        }
    }

}
