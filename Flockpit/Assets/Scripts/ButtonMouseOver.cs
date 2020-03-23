using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMouseOver : MonoBehaviour
{
    // Start is called before the first frame update
    private bool soundPlayed = false;
    private Color grey = new Color(201, 201, 201);
    private Color white = new Color(255, 255, 255);
    private void OnMouseOver()
    {
        if (soundPlayed == false)
        {
            GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<AudioManager>().PlaySound("Menu_Mouse_Over_01");
            this.gameObject.GetComponent<Renderer>().material.color = grey;
            soundPlayed = true;
        }
    }
    private void OnMouseExit()
    {
        soundPlayed = false;
        this.gameObject.GetComponent<Renderer>().material.color = white;
    }
}
