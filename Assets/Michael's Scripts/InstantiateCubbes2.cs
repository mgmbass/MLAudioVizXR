using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubbes2 : MonoBehaviour
{
    public float scale;
    public GameObject _cubePrefab;
    GameObject[] cubes = new GameObject[1024];
    Material cubeColor;
    // Start is called before the first frame update
    void Start()
    {
        //cubeColor = GetComponent<Material>();
        for (int i = 0; i < 1024; i++)
        {
            GameObject _sampleCubeInstance = (GameObject)Instantiate(_cubePrefab);
            _sampleCubeInstance.transform.position = this.transform.position;
            _sampleCubeInstance.transform.parent = this.transform;
            _sampleCubeInstance.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, 0, -0.703f * i);
            _sampleCubeInstance.transform.position = Vector3.up;
            cubes[i] = _sampleCubeInstance;
             
        }
    }

    // Update is called once per frame
    void Update()
    {

        float h = (10 * Mathf.Sin(Time.fixedTime) / 10);
        float p = (10 * Mathf.Sin(Time.fixedTime / 10) / 10);
        float sampleValue;
        for (int i = 0; i < 1024; i++)
        {
            sampleValue = Visualize2.buffers[i];
            if (sampleValue > 0.02f)
                sampleValue = sampleValue / 10;
            if (cubes[i] != null)
            {

                cubeColor = cubes[i].GetComponent<Renderer>().material;
                cubeColor.color = new Color(sampleValue * 1000, 1, h, 2.0f);
                cubes[i].transform.localScale = new Vector3(10, (sampleValue * scale), Visualize2.samples[2] * 100);



            }
        }

    }
}