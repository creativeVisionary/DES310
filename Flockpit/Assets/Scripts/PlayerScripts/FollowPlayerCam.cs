using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    Transform playerPos;
    Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        //Look at code based on code examples found at https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
        //Tag code based on examples found at https://docs.unity3d.com/ScriptReference/GameObject.FindWithTag.html?_ga=2.161388221.1896626259.1580218047-1376129383.1580218047
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        cameraTransform = this.transform;
        cameraTransform.LookAt(playerPos);
    }
}
