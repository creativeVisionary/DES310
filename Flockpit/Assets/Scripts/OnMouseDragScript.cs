using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseDragScript : MonoBehaviour
{
    public GameObject eventText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDrag()
    {
        //move the passenger along the x axis
        
        gameObject.transform.position = new Vector3((Input.mousePosition.x - 564)/20, 0, gameObject.transform.position.z);
    }

    void OnTriggerEnter(Collider col)
    {
        //if (col.collider.tag == "redBox")
        //{
        //    eventText.SetActive(true);
        //}
        if (col.tag == "Red")
        {
            if (gameObject.tag == "Red")
            {
                eventText.GetComponent<TextMesh>().text = "Correct";
            }
            if (gameObject.tag == "Blue")
            {
                eventText.GetComponent<TextMesh>().text = "Wrong";
            }

        }
        else if (col.tag == "Blue")
        {
            if (gameObject.tag == "Red")
            {
                eventText.GetComponent<TextMesh>().text = "Wrong";
            }
            if (gameObject.tag == "Blue")
            {
                eventText.GetComponent<TextMesh>().text = "Correct";
            }
        }
        eventText.SetActive(true);

    }
}
