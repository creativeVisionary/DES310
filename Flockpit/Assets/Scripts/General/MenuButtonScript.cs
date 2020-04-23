using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuButtonScript : MonoBehaviour
{

    private void Start()
    {
    }

    public void changeScene(string sceneName)
    {
        Debug.Log("Scene Changing To:" + sceneName);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

}
