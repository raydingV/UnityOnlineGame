using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI pingText;

    [SerializeField] private GameObject StartButton;

    private float SpawnPointX = 25;
    
    static GameManager manager = null;
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

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
        
        StartButton.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
       Debug.Log("Joined the " + PhotonNetwork.CurrentRoom);
       text.text = "Status: " + "Joined the " + PhotonNetwork.CurrentRoom;

       PhotonNetwork.Instantiate("OldCar", new Vector3(SpawnPointX - PhotonNetwork.CountOfPlayersInRooms * 5,0,0), Quaternion.identity);
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
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinOrCreateRoom("room1", new RoomOptions(), TypedLobby.Default);
            SceneManager.LoadScene("GameScene");
        }
    }

    private void Update()
    {
        if (countText != null)
        {
            countText.text = "There is a " + PhotonNetwork.CountOfRooms + " room & " + PhotonNetwork.CountOfPlayersOnMaster + " players are online";

        }

        if (pingText != null)
        {
            pingText.text = "Ping: " + PhotonNetwork.GetPing();
        }

    }
}
