using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public PhotonView gameView;

    public static bool connectedToMaster = false;


    public static string Platform = "Desktop";

    string room = "";

    bool m_createdRoom = false;

    public static event Action OnRoomCreated;
    public static event Action OnExistingRoomJoined;
    public static event Action OnPlayersChanged;

    public static NetworkManager Instance;

    public static string roomID = "";

    public static int TTL = 10;

    public static bool recentlyRejoined = false;

    public static int playerIndex;

    public Dictionary<string, int> roomState = new Dictionary<string, int>();

    public Dictionary<int, int> tasksState = new Dictionary<int, int>();

    List<Coroutine> countdownRoutines = new List<Coroutine>();

    private void Awake()
    {
        Instance = this;
    }


    //public void InitialiseRoomState()
    //{
    //    print("INITIALISED ROOM STATE");
    //    roomState.Add("usersVoted", 0);
    //    if (!roomState.ContainsKey("gameStarted"))
    //    {
    //        roomState.Add("gameStarted", 0);
    //    }
    //    roomState.Add("currentRound", 0);
    //    roomState.Add("currentEvent", 0);
    //}

    //public void UpdateTasksState(int role, int action)
    //{
    //    if (!tasksState.ContainsKey(role))
    //    {
    //        tasksState.Add(role, action);
    //    }
    //    else
    //    {
    //        tasksState[role] = action;
    //    }

    //}

    //private void ResetRoomData()
    //{
    //    tasksState.Clear();
    //    //InitialiseRoomState();
    //}



    private void Start()
    {

        Cursor.lockState = CursorLockMode.None;

        if (!PhotonNetwork.IsConnected)
            StartConnecting();


    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //if (!PhotonNetwork.IsConnected)
            //StartConnecting();
    }


    //string[] roleArray = { "Captain", "Sailing Master", "Gunner", "Swabbie" };

    //void SetupGame()
    //{

    //    PhotonNetwork.CurrentRoom.IsVisible = false;

    //    for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
    //    {
    //        gameView.RPC("AssignRole", PhotonNetwork.PlayerList[i], i);
    //    }



    //    gameView.RPC("MasterTeamName", RpcTarget.MasterClient);

    //}


    //public void GameEnded()
    //{
    //    PhotonNetwork.CurrentRoom.IsOpen = false;
    //    TTL = 0;
    //}


    //IEnumerator countdownCoroutine;

    ////IEnumerator StartCountdown()
    ////{

    ////    for (int i = 60; i > 5; i--)
    ////    {
    ////        LobbyUI.Instance.countdownText.text = i.ToString();
    ////        yield return new WaitForSeconds(1);
    ////    }


    ////    SetupGame();

    ////    for (int i = 5; i > 0; i--)
    ////    {
    ////        LobbyUI.Instance.countdownText.text = i.ToString();
    ////        yield return new WaitForSeconds(1);
    ////    }

    ////    InitialiseGame();
    ////}


    ////IEnumerator shortCountdownCoroutine;

    ////IEnumerator StartShortCountdown()
    ////{
    ////    SetupGame();


    ////    for (int i = 5; i > 0; i--)
    ////    {
    ////        LobbyUI.Instance.countdownText.text = i.ToString();
    ////        yield return new WaitForSeconds(1);
    ////    }


    ////    InitialiseGame();
    ////}


    //    void InitialiseGame()
    //{
        


    //    SceneManager.LoadScene(2);
    //    roomState["gameStarted"] = 1;

    //    roomState["usersVoted"] = 0;

    //    SetTTL(100000);
    //}
    

    //int SetTTL(int time)
    //{
    //    TTL = time;
    //    PhotonNetwork.CurrentRoom.PlayerTtl = TTL * 1000;

    //    return time;
    //}



    //[PunRPC]
    //void AssignRole(int roleID)
    //{
    //    PlayerInfo.role = roleID;
    //}


    //[PunRPC]
    //void MasterTeamName()
    //{

    //    PlayerInfo.GenerateName();
    //    LobbyUI.Instance.teamName.text = PlayerInfo.teamName;
    //    gameView.RPC("SetTeamName", RpcTarget.Others, PlayerInfo.teamName);

    //}


    //[PunRPC]
    //void SetTeamName(string masterName)
    //{

    //    PlayerInfo.teamName = masterName;
    //    LobbyUI.Instance.teamName.text = PlayerInfo.teamName;

    //}

    //public void ToggleVote(bool voteOn)
    //{
    //    print("CHANGING VOTE COUNT");

    //    if (voteOn)
    //    {
    //        gameView.RPC("IncrementVoteCount", RpcTarget.All, playerIndex);
    //    }
    //    else
    //    {
    //        gameView.RPC("DecrementVoteCount", RpcTarget.All, playerIndex);
    //    }
    //}


    //void UpdateVotesOnUI()
    //{
    //    LobbyUI.Instance.UpdateNumberOfUsers();



    //    //LobbyUI.Instance.UpdateVoteCountText(roomState["usersVoted"] + "/" + PhotonNetwork.CurrentRoom.PlayerCount);
    //}

    //void UpdateUsersInLobbyOnUI()
    //{
    //    LobbyUI.Instance.UpdateLobbyUsersText("Lobby Users: " + PhotonNetwork.CurrentRoom.PlayerCount + "/4");
    //}

    //[PunRPC]
    //void StopCount()
    //{
    //    foreach (var item in countdownRoutines)
    //    {
    //        StopCoroutine(item);
    //    }

    //    countdownRoutines.Clear();


    //    PhotonNetwork.CurrentRoom.IsVisible = true;

    //    if(LobbyUI.Instance.countdownText != null)
    //    LobbyUI.Instance.countdownText.text = "x";

    //    //if(LobbyUI.Instance.voteToggle != null)
    //    //LobbyUI.Instance.voteToggle.isOn = false;



    //    //if (LobbyUI.Instance.voteToggles != null)
    //    //{
    //    //    for (int i = 0; i < LobbyUI.Instance.voteToggles.Length; i++)
    //    //    {
    //    //        LobbyUI.Instance.ChangeVoteToggleState(false, i);
    //    //    }
    //    //}


    //    //roomState["usersVoted"] = 0;
    //    UpdateVotesOnUI();

    //}

    //[PunRPC]
    //void IncrementVoteCount(int playerNum)
    //{
    //    roomState["usersVoted"]++;


    //    LobbyUI.Instance.ChangeVoteToggleState(true, playerNum);

    //    UpdateVotesOnUI();

    //    CheckVotes();
    //}


    //[PunRPC]
    //void DecrementVoteCount(int playerNum)
    //{
    //    if(roomState["usersVoted"] == PhotonNetwork.CurrentRoom.PlayerCount/* && PhotonNetwork.CurrentRoom.PlayerCount > 1*/)
    //    {
    //        LobbyUI.Instance.countdownText.text = "x";
    //    }


    //    StopCoroutine(countdownCoroutine);
    //    PhotonNetwork.CurrentRoom.IsVisible = true;

    //    roomState["usersVoted"]--;

    //    LobbyUI.Instance.ChangeVoteToggleState(false, playerNum);

        

    //    UpdateVotesOnUI();

    //    Invoke("ResetCountdownText", 2f);
    //}


    //void ResetCountdownText()
    //{
    //    LobbyUI.Instance.countdownText.text = "";
    //}

    //void CheckVotes()
    //{

    //    if (roomState["usersVoted"] == PhotonNetwork.CurrentRoom.PlayerCount /*&& PhotonNetwork.CurrentRoom.PlayerCount > 1*/)
    //    {
    //        countdownRoutines.Add(StartCoroutine(StartCountdown()));
    //    }

    //}


    //[PunRPC]
    //void ReceiveRoomInfo(Dictionary<string, int> state)
    //{

    //    roomState = state;

    //    LoadRoomState();
    //}

    //[PunRPC]
    //void ReceiveTaskInfo(Dictionary<int, int> state)
    //{
    //    tasksState = state;

    //    StartCoroutine(LoadTasksState());
    //}


    //IEnumerator LoadTasksState()
    //{
    //    yield return new WaitForSeconds(8f);

    //    print("TRIED LOAD TASK STATE " + tasksState.Count);

    //    foreach (var item in tasksState)
    //    {
    //        print("Item TASK STATE: Key-" + item.Key.ToString() + "  Value-" + item.Value);
    //        PlayerPanelUpdater.Instance.UpdateRoleTaskText(item.Key, item.Value);
    //    }

    //    //foreach (var item in tasksState)
    //    //{
    //    //    print("Item TASK STATE: Key-" + item.ToString() + "  Value-" + item.Value);
    //    //}

    //    //for (int i = 0; i < tasksState.Count; i++)
    //    //{
    //    //    print("TASK role action " + i + tasksState[i]);
    //    //    GameController.Instance.UpdateRoleTaskText(i, tasksState[i]);
    //    //}

    //}

    //void LoadRoomState()
    //{
    //    UpdateVotesOnUI();


    //    if (roomState["gameStarted"] == 1)
    //    {
            
    //        //SceneManager.LoadScene(2);
    //        SetTTL(100000);
    //    }
    //    else
    //    {
    //        SetTTL(10);
    //    }


    //    print("loaded room state" + roomState["gameStarted"]);
    //}

    //[PunRPC]
    //    public void LeaveRoom()
    //{


    //    //StopCoroutine(countdownCoroutine);

    //    if(playerIndex == 0)
    //    {
    //        SetTTL(0);
    //        gameView.RPC("HostLeft", RpcTarget.Others);
    //    }


    //    AudioController.Instance.StopAllSounds();

    //    gameView.RPC("StopCount", RpcTarget.All);
    //    PhotonNetwork.LeaveRoom();



    //}

    //[PunRPC]
    //void PlayerJoinedCountdownCheck()
    //{
    //    if (roomState["gameStarted"] == 0)
    //    {
    //        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 || PhotonNetwork.CurrentRoom.PlayerCount == 3)
    //        {
    //            countdownRoutines.Add(StartCoroutine(StartCountdown()));
    //            LobbyUI.Instance.playerMessage.text = "Say Ahoy To Your New Crew!";
    //        }
    //        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
    //        {
    //            StopCount();

    //            shortCountdownCoroutine = StartShortCountdown();
    //            StartCoroutine(shortCountdownCoroutine);
    //        }
    //    }
    //}

    //[PunRPC]
    //void PlayerLeftCountdownCheck()
    //{
    //    if (roomState["gameStarted"] == 0)
    //    {
    //        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
    //        {
    //            StopCount();
    //            LobbyUI.Instance.playerMessage.text = "Waiting For Other Players...";
    //            LobbyUI.Instance.countdownText.text = "x";
    //            Invoke("ResetCountdownText", 2f);
    //        }
    //        if (PhotonNetwork.CurrentRoom.PlayerCount == 3 || PhotonNetwork.CurrentRoom.PlayerCount == 2)
    //        {
    //            StopCount();
    //            if (shortCountdownCoroutine != null)
    //            StopCoroutine(shortCountdownCoroutine);
    //            LobbyUI.Instance.countdownText.text = "x";

    //            countdownRoutines.Add(StartCoroutine(StartCountdown()));
    //        }
    //    }
    //}

    //[PunRPC]
    //void HostLeft()
    //{
    //    SetTTL(0);
    //    LeaveRoom();
    //}


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (otherPlayer == PhotonNetwork.PlayerList[0])
        {
            //gameView.RPC("HostLeft", RpcTarget.All);
        }

        //gameView.RPC("PlayerLeftCountdownCheck", RpcTarget.All);

        //for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        //{
        //    if (PhotonNetwork.PlayerList[i] == otherPlayer)
        //    {
        //        Destroy(GameObject.FindGameObjectWithTag(PlayerInfo.ConvertRoleToString(i)));
        //    }
        //}

        //SetPlayerIndex();
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        //UpdateUsersInLobbyOnUI();
        //UpdateVotesOnUI();

        //if (PhotonNetwork.IsMasterClient)
        //{
        //    gameView.RPC("ReceiveRoomInfo", newPlayer, roomState);
        //    gameView.RPC("ReceiveTaskInfo", newPlayer, tasksState);


        //    gameView.RPC("StopCount", RpcTarget.All);


        //    gameView.RPC("PlayerJoinedCountdownCheck", RpcTarget.All);
        //}

        //print(newPlayer.ActorNumber + " - " + newPlayer.NickName + " has joined the room");

        ////ThIS NEEDS TO BE UPDATED TO REMOVE THE ABILITY TO VOTE IF ALL PLAYERS LEAVE EXCEPT 1
        //if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        //{
        //    LobbyUI.Instance.VoteActiveState(true);
        //}


        //SetPlayerIndex();
    }


    




    public override void OnLeftRoom()
    {
        base.OnLeftRoom();



        //PhotonVoiceManager.Instance.DisconnectedFromRoom();

        //LobbyUI.SetTimeToRejoin(roomState["gameStarted"]);

        //ResetRoomData();


        //SceneManager.LoadScene(1);

    }



    public void StartConnecting()
    {
        if (PhotonNetwork.IsConnected)
        {
            OnConnectedToMaster();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        Debug.Log("Connecting To Master...");
        //LobbyUI.Instance.AddLog("Connecting To Master...");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        connectedToMaster = true;
        //if(LobbyUI.Instance.joinButton != null)
        //LobbyUI.Instance.joinButton.SetActive(true);

        JoinRandomRoom();
    }



    public void JoinRandomRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            StartConnecting();
            Invoke("JoinRandomRoom", 2f);
            return;
        }
        //roomState["gameStarted"] = 0;

        recentlyRejoined = false;

        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = null;
        byte expectedMaxPlayers = 4;

        MatchmakingMode matchingType = MatchmakingMode.FillRoom;
        TypedLobby typedLobby = null;
        string sqlLobbyFilter = null;
        string roomName = null;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.PlayerTtl = TTL * 1000;
        string[] expectedUsers = null;
        roomOptions.MaxPlayers = 4;

        PhotonNetwork.JoinRandomOrCreateRoom(expectedCustomRoomProperties, expectedMaxPlayers, matchingType, typedLobby, sqlLobbyFilter, roomName, roomOptions, expectedUsers);
    }


    //IEnumerator JoinedRoom()
    //{
    //    //SetTTL(10);

    //    //if(roomState.Count > 0)
    //    //if (roomState["gameStarted"] == 1)
    //    //{
    //    //    PhotonNetwork.IsMessageQueueRunning = false;

    //    //    SceneManager.LoadScene(2);

    //    //    while(SceneManager.GetActiveScene().buildIndex != 2)
    //    //    {
    //    //        yield return null;
    //    //    }

    //    //    PhotonNetwork.IsMessageQueueRunning = true;
    //    //}

    //    //lobbyUI = LobbyUI.Instance;


    //    //roomID = PhotonNetwork.CurrentRoom.Name;
    //    //print(roomID + " ROOMID");
    //    //PhotonVoiceManager.Instance.ConnectedToARoom(roomID);


    //    //UpdateUsersInLobbyOnUI();
    //    //UpdateVotesOnUI();
    //    //Debug.Log("Joined room!");
    //    //LobbyUI.Instance.AddLog("Joined room! '" + PhotonNetwork.CurrentRoom.Name + "'");


    //    //if (LobbyUI.Instance.lobbyScreen != null)
    //    //{
    //    //    LobbyUI.Instance.lobbyScreen.SetActive(true);
    //    //    LobbyUI.Instance.splashScreen.SetActive(false);
    //    //}

    //    //if (m_createdRoom)
    //    //{
    //    //    NetworkManager.OnRoomCreated?.Invoke();
    //    //}
    //    //else
    //    //{
    //    //    NetworkManager.OnExistingRoomJoined?.Invoke();
    //    //}



    //    //if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
    //    //{
    //    //    LobbyUI.Instance.VoteActiveState(true);
    //    //}

    //    ////NetworkVoiceManager.Instance.JoinedRoom(room);

    //    //LobbyUI.Instance.voiceManager.recorder.TransmitEnabled = !PhotonVoiceManager.micMuted;


    //    //SetPlayerIndex();

    //    //DEV DEBUG, REMOVE FOR FINAL VERSION
    //    //InstantStart();

    //}

    //void InstantStart()
    //{
    //    shortCountdownCoroutine = StartShortCountdown();
    //    StartCoroutine(shortCountdownCoroutine);
    //}


    public override void OnJoinedRoom()
    {
        SetPlayerIndex();

        PlayerInstantiation.Instance.SpawnPlayer();

        BallOwnership.Instance.SetID(playerIndex);
    }


    void SetPlayerIndex()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
            {
                playerIndex = i;
            }
        }

        //PhotonNetwork.LocalPlayer.ActorNumber = playerIndex;

        print("PLAYER INDEX " + playerIndex);

        print("local player index" + PhotonNetwork.LocalPlayer.ActorNumber);
    }


    public void Rejoin()
    {
        //print(roomID + " USING ID");
        recentlyRejoined = true;
        PhotonNetwork.RejoinRoom(roomID);
    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        recentlyRejoined = false;
        Debug.LogWarning("Room Join Failed " + message);
        //LobbyUI.Instance.AddLog(String.Format("Room Join Failed " + message));
        m_createdRoom = true;
        Debug.Log("Creating Room..." + room);
        //LobbyUI.Instance.AddLog("Creating Room...'" + room + "'");
        PhotonNetwork.CreateRoom(room, new RoomOptions { MaxPlayers = 4, IsOpen = true, IsVisible = true }, TypedLobby.Default);

    }

}


public class RoomState
{
    public int usersVoted;

    public bool gameStarted;

    public RoomState()
    {
        gameStarted = false;
    }


}
