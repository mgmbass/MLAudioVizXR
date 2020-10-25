using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCubes : MonoBehaviour
{
    public int band;
    public int scale;
    Material cubeColor;
    public bool useBuffer = true;
    // Start is called before the first frame update
    void Start()
    {
        cubeColor = this.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float h = (10 * Mathf.Sin(Time.fixedTime) / 10);
        float p = (10 * Mathf.Sin(Time.fixedTime / 10) / 10);
        if (!useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, Visualize3.freqband[band] * scale + 5, transform.localScale.z);
            transform.localPosition = new Vector3(transform.localPosition.x, (Visualize3.freqband[band] * scale / 2), transform.position.z);
        }

        if (useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, Visualize3.bandBuffer[band] * scale + 5, transform.localScale.z);
            transform.localPosition = new Vector3(transform.localPosition.x, (Visualize3.bandBuffer[band] * scale / 2), transform.position.z);
        }
        



        cubeColor.color = new Color(Visualize3.freqband[band] * 1000, h, p, 1.0f);


      }
      
}
