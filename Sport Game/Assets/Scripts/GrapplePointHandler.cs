using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class GrapplePointHandler : MonoBehaviour
{

    GameObject grapplingHook;

    GameObject attachedObj;

    Rigidbody rigidbody;

    SpringJoint spring;

    FixedJoint fixedJoint;

    GameObject xrRig;


    float damperStrength = 1;
    float springStrenth = 2000;



    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        spring = GetComponent<SpringJoint>();
    }


    public void GetGrappleReference(GameObject sentGrapplingHook, GameObject hitObj, GameObject playerBody)
    {

        grapplingHook = sentGrapplingHook;

        attachedObj = hitObj;

        xrRig = playerBody;

        hooked = true;

        

        spring = gameObject.AddComponent<SpringJoint>();

        //print(Vector3.Distance(sentGrapplingHook.transform.position, hitObj.transform.position));

        spring.spring = springStrenth / ((Vector3.Distance(grapplingHook.transform.position, attachedObj.transform.position)) * 100);
        spring.autoConfigureConnectedAnchor = false;
        spring.damper = damperStrength;

        spring.connectedBody = xrRig.GetComponent<Rigidbody>();



        if (attachedObj.TryGetComponent(out Rigidbody attachedRigibody))
        {

            fixedJoint = gameObject.AddComponent<FixedJoint>();


            fixedJoint.connectedBody = attachedRigibody;


            Invoke("KillObject", 4f);

        }
        else
        {
            rigidbody.isKinematic = true;
            transform.parent = hitObj.transform;


            Invoke("KillObject", 8f);
        }




    }

    bool hooked = false;

    
    public void UnZip()
    {
        print("grip");

        spring.spring = springStrenth / ((Vector3.Distance(grapplingHook.transform.position, attachedObj.transform.position)) * 100);

        spring.damper = damperStrength;
    }

    public void Zip()
    {
        print("ungrip");


        if (attachedObj.TryGetComponent(out Rigidbody attachedRigibody) == false)
        {

            xrRig.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            print(Vector3.Distance(grapplingHook.transform.position, attachedObj.transform.position));

            spring.spring = 120000 / ((Vector3.Distance(grapplingHook.transform.position, attachedObj.transform.position)) * 100);

            spring.damper = 50;


            Invoke("UnZip", 0.1f);

        }
        else
        {
            attachedRigibody.velocity = new Vector3(0, 0, 0);
            attachedRigibody.angularVelocity = Vector3.zero;

            KillObject();
            

        }

            
        



    }



    private void FixedUpdate()
    {
        if (hooked)
        {
            spring.connectedAnchor = xrRig.transform.InverseTransformPoint(grapplingHook.transform.position);

            //print(spring.connectedAnchor);

            //print(grapplingHook.transform.position);
            //print(xrRig.transform.position);
        }
    }



    void KillObject()
    {
        grapplingHook.GetComponent<GrapplingHook>().ReleaseHook();
    }

}
