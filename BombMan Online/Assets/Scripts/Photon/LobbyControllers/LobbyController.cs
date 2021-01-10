using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private int roomSize;

    void Start()
    {
       
       PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true; 
    }

    public void NewConnection()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No aviable rooms");
        CreateRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Non existing Room or Full");
    }
    private void CreateRoom()
    {
        Debug.Log("Create own random room");
        int randomNum = Random.Range(0, 1000);
        RoomOptions rOpts = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom("BomberDroid" + randomNum, rOpts);
    }

    public void CreateRoomWithTag(string value)
    {
        Debug.Log("Create own room");
        RoomOptions rOpts = new RoomOptions() { IsVisible = false, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(value, rOpts);
    }

    public void JoinRoomWithTag(string value)
    {
        PhotonNetwork.JoinRoom(value);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("fail create room");
        CreateRoom();
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
