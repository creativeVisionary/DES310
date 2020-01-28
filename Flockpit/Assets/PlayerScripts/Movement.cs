using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController charController;
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void handleInput()
    {
        //Basic Movement
        //Based on code examples found at https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
        //Input code based on examples found at https://docs.unity3d.com/ScriptReference/Input.html
        //Tag code based on examples found at https://docs.unity3d.com/ScriptReference/GameObject.FindWithTag.html?_ga=2.161388221.1896626259.1580218047-1376129383.1580218047
        //Utilizises Unity default Input found in Project Settings->Input
        float forwardAxis = Input.GetAxis("Horizontal");
        float sideAxis = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(forwardAxis, 0.0f, sideAxis);
        moveDir *= 5.0f;
        //Gravity
        //Will not work if the game object has no collision component
        if (!charController.isGrounded)
        {
            float gravForce = 3.0f;
            moveDir.y = -1 * gravForce;
        }

        charController.Move(moveDir * Time.deltaTime);
        //Reset Position
        if (Input.GetMouseButton(1))
        {
            gameObject.transform.position = new Vector3(0.0f, 5.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }
}
