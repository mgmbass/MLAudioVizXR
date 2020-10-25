using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rotate : MonoBehaviour
{
    private int angle;
    private GameObject cube;
    
    void Start()
    {

        cube = GameObject.Find("CubePrefab");
        angle = 0;
        cube.transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(rotate());
    }

    IEnumerator rotate()
    {
        
        cube.transform.Rotate(0,0,0.5f);
        yield return new WaitForSecondsRealtime(0.01f);

        

    }
}
