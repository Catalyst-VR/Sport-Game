using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using XRController = UnityEngine.XR.Interaction.Toolkit.XRController;

public class IndividualPlayerSetup : MonoBehaviour
{

    public PhotonView view;

    [SerializeField]
    Vector3[] startingPositions;

    [SerializeField]
    Vector3[] startingRotations;


    [Header("Components To Disable")]

    [SerializeField]
    Camera camera;


    [SerializeField]
    UnityEngine.SpatialTracking.TrackedPoseDriver poseDriver;

    [SerializeField]
    XRController[] controllers;


    [SerializeField]
    LocomotionSystem locomotionSystem;


    [SerializeField]
    PhysicsHand[] physicsHands;


    [SerializeField]
    GrapplingHook[] grapplingHooks;

    int playerID;

    // Start is called before the first frame update
    void Awake()
    {

        playerID = 0;


        playerID = Int32.Parse(view.ViewID.ToString()[0].ToString());


        if (!view.IsMine)
        {
            DisablePlayer();
        }

        SetStartingPosition();

    }

    void DisablePlayer()
    {
        camera.enabled = false;

        

        poseDriver.enabled = false;

        foreach (var item in controllers)
        {
            item.enabled = false;
        }

        foreach (var item in physicsHands)
        {
            item.enabled = false;
        }

        foreach (var item in grapplingHooks)
        {
            item.enabled = false;
        }

        locomotionSystem.enabled = false;

    }


    void SetStartingPosition()
    {

        transform.position = startingPositions[playerID - 1];

        transform.rotation = Quaternion.Euler(startingRotations[playerID - 1]);

    }

    
}
