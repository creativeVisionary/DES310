using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoUIScript : MonoBehaviour
{
    //For Prefab Use Only
    public Canvas uiCanvas;
    public UnityEngine.UI.Text titleText;

    private string gameTitle;
    private string sceneName;

    public void SetDetails(string gameT, string sceneN)
    {
        gameTitle = gameT;
        sceneName = sceneN;
        titleText.text = gameTitle;

    }


   public void StartButton()
    {
        Debug.Log("Scene Changing To:" + sceneName);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

   public void QuitButton()
    {
        Destroy(this.gameObject);
    }
}
