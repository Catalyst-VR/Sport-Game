using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingHook : MonoBehaviour
{
    enum Hand { Right, Left };



    [SerializeField] Hand handedness;

    [SerializeField] private InputActionAsset actionAsset;

    [SerializeField] GameObject grapplePoint;

    [SerializeField] GameObject playerBody;

    [SerializeField] PowerFist powerFist;

    [SerializeField] MeshRenderer meshRenderer;


    bool zipUsed = false;

    bool grappleCooling = false;

    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();


        InputAction trigger;
        InputAction release;
        InputAction grip;
        InputAction ungrip;

        if (handedness == Hand.Right)
        {
            trigger = actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Activate");
            release = actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Release");
            grip = actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Select");
            ungrip = actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Deselect");
        }
        else
        {
            trigger = actionAsset.FindActionMap("XRI LeftHand Interaction").FindAction("Activate");
            release = actionAsset.FindActionMap("XRI LeftHand Interaction").FindAction("Release");
            grip = actionAsset.FindActionMap("XRI LeftHand Interaction").FindAction("Select");
            ungrip = actionAsset.FindActionMap("XRI LeftHand Interaction").FindAction("Deselect");
        }

        


        trigger.Enable();
        trigger.performed += ShootHook;


        release.Enable();
        release.performed += Release;


        grip.Enable();
        grip.performed += GripPush;


        ungrip.Enable();
        ungrip.performed += GripRelease;
    }


    GameObject grapplePointObj;

    bool hookSpawned = false;



    void GripPush(InputAction.CallbackContext context)
    {
        if (hookSpawned)
        {
            if(!zipUsed)
            {
                if (grapplePointObj != null)
                {
                    zipUsed = true;
                    grapplePointObj.GetComponent<GrapplePointHandler>().Zip();
                }
            }
        }
        else
        {
            powerFist.EnableFist();
        }
    }

    void GripRelease(InputAction.CallbackContext context)
    {
        if (hookSpawned)
        {
            
        }
        else
        {
            powerFist.DisableFist();
        }
    }




    void ShootHook(InputAction.CallbackContext context)
    {


        if (!grappleCooling)
        {
            print("Grapple " + handedness.ToString());

            RaycastHit hit;

            int layerMask = 1 << 6;
            layerMask = ~layerMask;


            if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity, layerMask))
            {

                line.positionCount = 2;

                hookSpawned = true;

                grapplePointObj = Instantiate(grapplePoint, hit.point, Quaternion.Euler(transform.forward));


                grapplePointObj.GetComponent<GrapplePointHandler>().GetGrappleReference(gameObject, hit.transform.gameObject, playerBody);


            }

        }

    }


    void CoolGrapple()
    {
        grappleCooling = false;

         UnityEngine.Color color;

        if (ColorUtility.TryParseHtmlString("#8E7B6A", out color))
        { meshRenderer.material.color = color; }
        
    }

    void Release(InputAction.CallbackContext context)
    {
        ReleaseHook();
    }



    public void ReleaseHook()
    {
        print("Release " + handedness.ToString());
        grappleCooling = true;
        meshRenderer.material.color = UnityEngine.Color.red;
        Invoke("CoolGrapple", 5f);


        hookSpawned = false;
        zipUsed = false;

        Destroy(grapplePointObj);

        line.positionCount = 0;
    }

    private void Update()
    {
        if (hookSpawned)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, grapplePointObj.transform.position);
        }
        
    }


}
