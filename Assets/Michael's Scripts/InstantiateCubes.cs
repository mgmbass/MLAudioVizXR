using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MagicLeapTools;
public class InstantiateCubes : MonoBehaviour
{
    public float scale;
    public GameObject _cubePrefab;
    TransmissionObject[] cubes = new TransmissionObject[512];
    Material cubeColor;
    TransmissionObject _transmissionCube;
 
    // Start is called before the first frame update
    void Start()
    {
        //cubeColor = GetComponent<Material>();
        for(int i = 0; i<512; i++)
        {
            TransmissionObject _sampleCubeInstance = Transmission.Spawn("Transmission Cube",this.transform.position, Quaternion.identity,this.transform.lossyScale);//(GameObject)Instantiate(_cubePrefab);
            _sampleCubeInstance.transform.localPosition = this.transform.position;
            _sampleCubeInstance.transform.parent = this.transform;
            _sampleCubeInstance.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0,0,i*10);
            _sampleCubeInstance.transform.position = Vector3.forward;
            cubes[i] = _sampleCubeInstance;

        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
        float h = (10 * Mathf.Sin(Time.fixedTime) / 10);
        float p = (10 * Mathf.Sin(Time.fixedTime / 10) / 10);

        float sampleValue;
        for(int i = 0; i<512; i++)
        {
            sampleValue = Visualize.buffers[i];
            if (sampleValue > 0.002f)
                sampleValue = sampleValue/10;
            if (i > 100)
                sampleValue *= 10;
            if (cubes[i] != null)
            {

                cubeColor = cubes[i].GetComponent<Renderer>().material;
                cubeColor.color = new Color(sampleValue * 100, p, h, 1.0f);
                cubes[i].transform.localScale = new Vector3(10, (sampleValue * scale), 10);
               

                
            }
        }
        
    }

    
}
