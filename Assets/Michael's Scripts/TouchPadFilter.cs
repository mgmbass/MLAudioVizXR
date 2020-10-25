using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;
using UnityEngine;

public class TouchPadFilter : MonoBehaviour
{

    private MLInputController _controller;
    private AudioHighPassFilter hpf;
    private AudioLowPassFilter lpf;
    private bool active;
    private float currentPosx;
    private float currentPosY;

    // Start is called before the first frame update
    void Start()
    {
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        hpf = GetComponent<AudioHighPassFilter>();
        lpf = GetComponent<AudioLowPassFilter>();
        active = false;
        currentPosx = 1;
        currentPosY = 0;
    }

    void OnDestroy()
    {
        MLInput.Stop();
    }


    // Update is called once per frame
    void Update()
    {
        doFilter();
    }


    public void setActive()
    {
        active = !active;
        if(active == false)
        {
            lpf.cutoffFrequency = 22000;
            hpf.cutoffFrequency = 0;
        }
    }



    void doFilter() {
        if (active)
        {
            if (currentPosx != _controller.Touch1PosAndForce.x || currentPosY != _controller.Touch1PosAndForce.y)
            {
                lpf.cutoffFrequency = 22000 * (_controller.Touch1PosAndForce.x + 1) / 2;
                currentPosx = _controller.Touch1PosAndForce.x;
                hpf.cutoffFrequency = 22000 * (_controller.Touch1PosAndForce.y + 1) / 2;
                currentPosY = _controller.Touch1PosAndForce.y;
            }
        }
    }

}
