using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GateStatus { CLOSED = -1, OPEN = 1 }

public class AnimationScript : MonoBehaviour
{
    public List<Animator> gateAnimators;
    public bool openGate = false;
    public bool closeGate = true;
    private int gateStatus = (int)GateStatus.CLOSED;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (gateStatus != (int)GateStatus.OPEN)
        {
            for (int i = 0; i < gateAnimators.Count; i++)
            {
                if (openGate == true)
                {
                    gateAnimators[i].Play("GateOpen");
                    gateStatus = (int)GateStatus.OPEN;
                }
            }
        }
            if (gateStatus != (int) GateStatus.CLOSED)
            {
            for (int i = 0; i < gateAnimators.Count; i++)
            {
                if (closeGate == true)
                {
                    gateAnimators[i].Play("GateClose");
                    gateStatus = (int)GateStatus.CLOSED;
                }
            }
        }

    }

    public void SetGateOpen()
    {
        openGate = true;
        closeGate = false;
    }
    public void SetGateClosed()
    {
        openGate = false;
        closeGate = true;
    }

}
