//
//  Code -> Joe Abbati - January, 2020
//
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;      // joa -> needed for Switching Scenes


public class MySceneManager : MonoBehaviour
{
    // joa -> Make me a Singleton
    private void Awake()
    {

        // This returns an array of existing Objects of this 'Type'
        int numSceneManagers = FindObjectsOfType<MySceneManager>().Length;

        if (numSceneManagers > 1)
            Destroy(gameObject);   // gameObject is "This" (Instance)
        else 
            DontDestroyOnLoad(gameObject); // gameObject is THIS 
    }
    // Start is called before the first frame update
    void Start()
    {
        // joa -> See Unity Doc's on Invoke method - we can't use with Parms, so make dedicated "First" Scene Load
        Invoke("LoadMainMenu", 5f);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public int GetCurrentSceneNumber()
    {
         int n = SceneManager.GetActiveScene().buildIndex;
         return n;
    }

    public void LoadSceneByIndex( int scene )
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadSceneByIndexWithDelay( int scene, float wait_time )
    {
        StartCoroutine(WaitCoRoutine( scene, wait_time));
    }
  
    IEnumerator WaitCoRoutine( int scene, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        // joa -> After delayTime, Load the Scene
        SceneManager.LoadScene(scene);
    }

    public void CallAppQuit()
    {
        Application.Quit();
    }
}
