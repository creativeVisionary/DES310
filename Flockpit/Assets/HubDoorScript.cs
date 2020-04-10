using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string gameTitle;
    public string sceneName;
    public GameObject gameUIPrefab;

    private void OnMouseDown()
    {

      GameObject gameDetailsUI =  Instantiate(gameUIPrefab);
        gameDetailsUI.GetComponent<GameInfoUIScript>().SetDetails(gameTitle, sceneName) ;
    }
}
