using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferForce : MonoBehaviour
{
    [SerializeField]
    Rigidbody mainBody;

    Rigidbody rigiHand;


    FixedJoint handJoint;


    // Start is called before the first frame update
    void Awake()
    {
        rigiHand = GetComponent<Rigidbody>();

        handJoint = GetComponent<FixedJoint>();
    }


    Vector3 handForce;

    private void FixedUpdate()
    {

        //print("Body Vel" + mainBody.velocity);


        handForce = handJoint.currentForce / 1000;

        if (handForce.x < 1) handForce.x = 0;
        if (handForce.y < 1) handForce.y = 0;
        if (handForce.z < 1) handForce.z = 0;

        if(handForce.x != 0 && handForce.y != 0 && handForce.x  != 0) print("FORCE " + handForce);

        mainBody.AddForce(handForce);


    }



}
