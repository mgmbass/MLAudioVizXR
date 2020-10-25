using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class AudioHandTrack : MonoBehaviour
{
    public enum HandPoses { Ok, Finger, Thumb, OpenHand, Fist, NoPose };
    public HandPoses pose = HandPoses.NoPose;
    public Vector3[] pos;

    AudioSource aS;
    AudioChorusFilter chorus;

    private MLHandKeyPose[] _gestures;

    private void Start()
    {
        MLHands.Start();
        _gestures = new MLHandKeyPose[5];
        _gestures[0] = MLHandKeyPose.Ok;
        _gestures[1] = MLHandKeyPose.Finger;
        _gestures[2] = MLHandKeyPose.OpenHand;
        _gestures[3] = MLHandKeyPose.Fist;
        _gestures[4] = MLHandKeyPose.Thumb;
        MLHands.KeyPoseManager.EnableKeyPoses(_gestures, true, false);
        pos = new Vector3[3];
        aS = GetComponent<AudioSource>();
        chorus = GetComponent<AudioChorusFilter>();
    }
    private void OnDestroy()
    {
        MLHands.Stop();
    }

    private void Update()
    {
        if (GetGesture(MLHands.Left, MLHandKeyPose.Ok))
        {
            pose = HandPoses.Ok;

        }
        else if (GetGesture(MLHands.Left, MLHandKeyPose.Finger))
        {
            pose = HandPoses.Finger;
            if (!aS.isActiveAndEnabled)
                aS.enabled = true;
            if(!aS.isPlaying)
                aS.Play();
        }
        else if (GetGesture(MLHands.Left, MLHandKeyPose.OpenHand))
        {
            pose = HandPoses.OpenHand;
            if (aS.isPlaying)
                aS.Stop();
        }
        else if (GetGesture(MLHands.Left, MLHandKeyPose.Thumb))
        {
            pose = HandPoses.Fist;
            if (!chorus.isActiveAndEnabled)
            {
                chorus.enabled = true;
                chorus.depth = 0.3f;
                chorus.rate = 2;
            }
         
        }
        else if (GetGesture(MLHands.Left, MLHandKeyPose.Fist))
        {
            pose = HandPoses.Thumb;
            if (chorus.isActiveAndEnabled)
            {
                chorus.enabled = false;
            }
        }
        else
        {
            pose = HandPoses.NoPose;
        }


    }



    private bool GetGesture(MLHand hand, MLHandKeyPose type)
    {
        if (hand != null)
        {
            if (hand.KeyPose == type)
            {
                if (hand.KeyPoseConfidence > 0.9f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void endAudio()
    {
        if (aS.isPlaying)
            aS.Stop();
    }
}

    

