using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private int roomSize;

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

    private void CreateRoom()
    {
        Debug.Log("Create own room");
        int randomNum = Random.Range(0, 1000);
        RoomOptions rOpts = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom("BomberDroid" + randomNum, rOpts);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("fail create room");
        CreateRoom();
    }
}
