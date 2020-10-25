using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MainMenueShow : MonoBehaviour
{
   public GameObject menue;
    private bool menueIsActive;

    // Start is called before the first frame update
    void Start()
    {
        MLInput.Start();
        //menue = GameObject.Find("MainMenuePanel");
        MLInput.OnControllerButtonUp += OnButtonUp;
        menueIsActive = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        MLInput.OnControllerButtonUp -= OnButtonUp;
        MLInput.Stop();
    }

    void OnButtonUp(byte controller_id, MLInputControllerButton button)
    {
        if ((button == MLInputControllerButton.HomeTap))
        {
            print("HomeTap");
            if (menueIsActive)
            {
                menue.SetActive(false);
                menueIsActive = false;
            }

            else
            {
                menue.SetActive(true);
                menueIsActive = true;
            }
        }
    }


}
