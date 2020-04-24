using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to handle the game info UI prefab functionality
public class GameInfoUIScript : MonoBehaviour
{
    //Member Variables
    //
    //Inspector Set parameters
    //Text Object
    public UnityEngine.UI.Text titleText;
    //Game Detail Variables
    //Strings to hold title of game(displayed to player) and the name of the scene for which the start button loads to
    private string gameTitle;
    private string sceneName;

    //Functions

    //Function to take the game title and scene name, displaying the game title and storing the name of the scene
    public void SetDetails(string gameT, string sceneN)
    {
        gameTitle = gameT;
        sceneName = sceneN;
        titleText.text = gameTitle;

    }

    //Function to execute when start button is pressed
    //Loads scene dictated by sceneName
   public void StartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    //Destroys UI when Exit button is pressed
   public void QuitButton()
    {
        Destroy(this.gameObject);
    }
}
