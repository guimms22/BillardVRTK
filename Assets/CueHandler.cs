using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueHandler : MonoBehaviour {

    //HTC VIVE

    //public SteamVR_TrackedObject frontController;
    //public SteamVR_TrackedObject backController;

    public Transform leftHand;
    public Transform rightHand;
    public Transform cueTip;



    private Rigidbody cueRB;
    


    private float lockOffset;
    private Vector3 cuePos;
    private Vector3 lockForward;
    // Use this for initialization
    void Start() {
        cueRB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        
        Debug.Log("Input:" + OVRInput.GetControllerPositionTracked(OVRInput.Controller.LTouch) + "  ->  "+ OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));

        UpdateCuePosition();
    }

    void UpdateCuePosition()
    {


        Vector3 frontPos = leftHand.transform.position;
        Vector3 backPos = rightHand.transform.position;

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            //print("first press");
            
            lockForward = transform.up;
            lockOffset = (frontPos - backPos).magnitude;
        }
        else if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            //print("held");
            float currOffset = (frontPos - backPos).magnitude;
            cueRB.MovePosition(cuePos + lockForward * (lockOffset - currOffset));
        }
        else
        {
            cuePos = 0.75f * backPos + 0.25f * frontPos;
            cueRB.MovePosition(cuePos);
            cueRB.MoveRotation(Quaternion.LookRotation(frontPos - backPos) * Quaternion.Euler(90f, 0f, 0f));
        }




        // HTC VIVE 

        //var front = SteamVR_Controller.Input((int)frontController.index);
        //var back = SteamVR_Controller.Input((int)backController.index);

        //   if (back.GetPressDown(SteamVR_Controller.ButtonMask.)) // First press of trigger
        //   {
        //       print("first press");
        //   }
        //   else if(back.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) // trigger  held down 
        //   {
        //       print("held");
        //   }
        //   else // free mode
        //{
        //       print("free");
        //   }


    }

    private void OnCollisionEnter(Collision col)
    {
        Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
        if (!rb )
        {
            return;
        }

        Vector3 forceDirection = (col.contacts[0].point - cueTip.position).normalized;

        rb.AddForce(forceDirection * cueRB.velocity.magnitude);
    }
}
