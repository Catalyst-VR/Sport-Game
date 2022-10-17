using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PlayerInstantiation : MonoBehaviour
{

    [SerializeField]
    List<GameObject> PlayerPrefabs = new List<GameObject>();


    [SerializeField]
    GameObject ball;

    public static PlayerInstantiation Instance;

    //GameObject[] activePlayerObjects = new GameObject[4];

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        //print("PLAYER INDEX: " + NetworkManager.playerIndex);

        //print("ACTOR NUM:" + PhotonNetwork.LocalPlayer.ActorNumber);

        //DisableUnusedGameControllers();

        //SpawnPlayer();
    }

    private void Start()
    {
        //SpawnBall();
    }

    void DisableUnusedGameControllers()
    {

        //foreach (var item in GameControllers)
        //{
        //    if(item.name != PhotonNetwork.LocalPlayer.ActorNumber.ToString())
        //    {
        //        item.SetActive(false);
        //    }
        //}


    }


    void SpawnBall()
    {
        PhotonNetwork.Instantiate(ball.name, new Vector3(-2.4000001f, 49.0999985f, 22.8999996f), Quaternion.EulerRotation(Vector3.zero));
    }


    public void SpawnPlayer()
    {

        //int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        

        PhotonNetwork.Instantiate(PlayerPrefabs[NetworkManager.playerIndex].name, GetPosFromRole(NetworkManager.playerIndex + 1), GetRotFromRole(NetworkManager.playerIndex + 1));

    }


    void SpawnDesktopPlayer()
    {

        //int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        //PhotonNetwork.Instantiate(DesktopPlayerPrefabs[NetworkManager.playerIndex].name, GetPosFromRole(NetworkManager.playerIndex + 1), GetRotFromRole(NetworkManager.playerIndex + 1));
    }


        Vector3 GetPosFromRole(int role)
    {
        Vector3 playerPos = new Vector3();

        switch (role)
        {
            case 1:
                playerPos = new Vector3(0.00800000038f, 1.2f, -2f);
                break;
            case 2:
                playerPos = new Vector3(-2.08599997f, 1.2f, -0.056f);
                break;
            case 3:
                playerPos = new Vector3(0f, 1.2f, 2f);
                break;
            case 4:
                playerPos = new Vector3(1.983f, 1.2f, 0f);
                break;
            default:
                break;
        }

        return playerPos;

    }


    Quaternion GetRotFromRole(int role)
    {

        Quaternion playerRot = new Quaternion();


        switch (role)
        {
            case 1:
                playerRot.eulerAngles = new Vector3(0, 0, 0);
                break;
            case 2:
                playerRot.eulerAngles = new Vector3(0, 90, 0);
                break;
            case 3:
                playerRot.eulerAngles = new Vector3(0, 180, 0);
                break;
            case 4:
                playerRot.eulerAngles = new Vector3(0, 270, 0);
                break;
            default:
                break;
        }

        return playerRot;
    }

}
