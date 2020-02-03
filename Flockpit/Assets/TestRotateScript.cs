using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotateScript : MonoBehaviour
{
    [Range(0.0f, 400.0f)]
    public float rotateSpeed = 0.0f;
    public bool manualControl = false;
    public Vector3 angleVel;
    Vector3 rotateAxis = new Vector3(0.0f, 1.0f, 0.0f);
    Rigidbody rigBody;
    // Start is called before the first frame update
    void Start()
    {
        rigBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        angleVel = rigBody.angularVelocity;
        if (angleVel.y < 2.5*rotateSpeed)
        {
            rigBody.AddTorque(0.0f, 1.0f*rotateSpeed, 0.0f);
        }
    }
}
