using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameControllerScript gameController;
    public GameObject controllerObject;
    public string objectString;
    void Start()
    {
        gameController = controllerObject.GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == objectString)
        {
            if (collision.gameObject.GetComponent<ObjectInteractCursorScript>().hasExpired != true)
            {
                gameController.incrimentPlayerScore(1);
                collision.gameObject.GetComponent<ObjectInteractCursorScript>().hasExpired = true;
            }
        }
    }
}
