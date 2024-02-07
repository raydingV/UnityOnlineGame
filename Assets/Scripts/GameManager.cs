using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI pingText;
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server");
        text.text = "Status: " + "Connected to Server";
        
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the Lobby");
        text.text = "Status: " + "Joined the Lobby";
    }

    public override void OnJoinedRoom()
    {
       Debug.Log("Joined the " + PhotonNetwork.CurrentRoom);
       text.text = "Status: " + "Joined the " + PhotonNetwork.CurrentRoom;

       PhotonNetwork.Instantiate("Cube", Vector3.zero, Quaternion.identity);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Join is failed because of " + "[" + returnCode + "]" + message);
        text.text = "Status: " + "Join is failed because of " + "[" + returnCode + "]" + message;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created");
        text.text = "Status: " + "Room Created";
    }

    public override void OnLeftRoom()
    {
        Debug.Log("You Left the Room");
        text.text = "Status: " + "You Left the Room";
    }

    public override void OnLeftLobby()
    {
        
    }

    public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void enterRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("room1", new RoomOptions(), TypedLobby.Default);
    }

    private void Update()
    {
        countText.text = "There is a " + PhotonNetwork.CountOfRooms + " room & " + PhotonNetwork.CountOfPlayersOnMaster + " players are online";

        pingText.text = "Ping: " + PhotonNetwork.GetPing();
    }
}
