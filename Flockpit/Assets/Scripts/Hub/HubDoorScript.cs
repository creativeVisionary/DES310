using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to handle interactions with hub game doors
public class HubDoorScript : MonoBehaviour
{
    //Member Variables
    //
    //Inspector Set parameters
    //String for the title of the game(displayed to the player) and the name of the scene object to be loaded when the start button is pressed
    public string gameTitle;
    public string sceneName;
    //Reference to game UI prefab
    public GameObject gameUIPrefab;

    //Functions

    //When mouse/screen tap is down over the button, check if the game UI is already open,
    //If it isn't, create a new UI and set the details
    private void OnMouseDown()
    {
        if (!CheckForUI())
        {
            GameObject gameDetailsUI = Instantiate(gameUIPrefab);
            gameDetailsUI.GetComponent<GameInfoUIScript>().SetDetails(gameTitle, sceneName);
        }
    }


    //Function to check if game info UI is already open
    bool CheckForUI()
    {
        //Gets a count of objects with the tag GameInfo in the scene
        //If there are any(uiCount above 0) then there is a UI open
        int uiCount = 0;
        uiCount = GameObject.FindGameObjectsWithTag("GameInfo").Length;
        if (uiCount > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
