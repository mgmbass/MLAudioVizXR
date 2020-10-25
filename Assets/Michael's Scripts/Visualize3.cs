using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualize3 : MonoBehaviour
{

    AudioSource _audioSource;
    AudioHighPassFilter hfFilter;
    AudioLowPassFilter lfFilter;
    public static float[] samples = new float[512];
    public static float[] freqband = new float[8];
    public static float[] bandBuffer = new float[8];
    public static float[] decreaseBuffer = new float[8];
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
        MakeFrequencyBands();
        buffer();
    }

    void GetSpectrumAudio()
    {
        _audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
        
    }

    void HighPassZeros()
    {
        int zeros = (int)hfFilter.cutoffFrequency / 42;
        for (int i = 0; i < zeros; i++)
        {
            samples[i] = 0.00001f;
        }
    }

    void LowPassZeros()
    {
        int zeros = (int)lfFilter.cutoffFrequency / 42;
        for (int i = 511; i >= zeros; i--)
        {
            samples[i] = 0.00001f;
        }
    }

    void MakeFrequencyBands()
    {

        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int samplecount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
            {
                samplecount += 2;
            }

            for (int j = 0; j < samplecount; j++)
            {
                average += samples[count] * count;
                count++;
            }
            average /= count;
            freqband[i] = average * 10;
        }
    }

    void buffer()
    {
        for(int i = 0; i < 8; i++)
        {
            if (freqband[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqband[i];
                decreaseBuffer[i] = 0.0005f;
            }

            if (freqband[i] < bandBuffer[i])
            {
                bandBuffer[i] -= decreaseBuffer[i];
                decreaseBuffer[i] *= 2.5f;
            }
        }
    }
}
