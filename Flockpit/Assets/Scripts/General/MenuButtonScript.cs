using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Simple script to change current scene to the scene with sceneName
public class MenuButtonScript : MonoBehaviour
{

    public void changeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

}
