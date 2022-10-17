using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallOwnership : MonoBehaviour
{

    [SerializeField]
    PhotonView view;

    public static BallOwnership Instance;


    string myID = "P1";

    private void Awake()
    {
        Instance = this;
    }


    public void SetID(int index)
    {
        switch (index)
        {
            case 0:
                myID = "P1";
                break;
            case 1:
                myID = "P2";
                break;
            case 2:
                myID = "P3";
                break;
            case 3:
                myID = "P4";
                break;

            default:
                myID = "P1";
                break;
        }
    }

    public void RequestOwnership()
    {
        view.RequestOwnership();
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == myID)
        {
            RequestOwnership();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == myID)
        {
            RequestOwnership();
        }
    }

}
