using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("RCP_UpdateLobby", RpcTarget.AllBufferedViaServer, PhotonNetwork.NickName);
            GameSetUpController.GS.myNetworkPlayer = gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Disconnect();
        }
    }

    public void StartGame()
    {
        PV.RPC("RCP_StartGame", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void RCP_StartGame()
    {
        GameTimer.GT.NewGame();

        GameSetUpController.GS.DisableCanvas();
        
        int id = GameSetUpController.GS.GetPosition(PhotonNetwork.NickName);
        myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player" + (id + 1).ToString()),
        GameSetUpController.GS.spawnPoints[id].position, GameSetUpController.GS.spawnPoints[id].rotation);
    }

    [PunRPC]
    void RCP_UpdateLobby(string name)
    {
        GameSetUpController.GS.PlayerConnected(name);
    }

    public void DestroyAvatar()
    {
        if(myAvatar != null)
        PV.RPC("RPC_DestroyAvatar", RpcTarget.AllViaServer);
    }

    [PunRPC]
    public void RPC_DestroyAvatar()
    {
       PhotonView newPV = myAvatar.GetComponent<PhotonView>();
        if(newPV != null)
           PhotonNetwork.Destroy(newPV);
    }


    public void Disconnect()
    {
        if (PhotonNetwork.IsMasterClient)
            PV.RPC("RPC_DisconnectAll", RpcTarget.AllBufferedViaServer);

        else
            PV.RPC("RPC_Disconnect", RpcTarget.AllBufferedViaServer, PhotonNetwork.NickName);

        GameObject.Find("RoomController").GetComponent<RoomController>().GoToMenu();
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    public void RPC_DisconnectAll()
    {
        if (name != PhotonNetwork.NickName)
        {
            GameObject.Find("RoomController").GetComponent<RoomController>().GoToMenu();
            PhotonNetwork.Destroy(gameObject);
        }
    }
    [PunRPC]
    public void RPC_Disconnect(string name)
    {
        if (name != PhotonNetwork.NickName)
            GameSetUpController.GS.DisconnectPlayer(name);
    }

    public void ReturnToLobby()
    {
        PV.RPC("RPC_ReturnToLobby", RpcTarget.All);

    }

    [PunRPC]
    public void RPC_ReturnToLobby()
    {
        WinLoseMenu.instance.ReturnToLobby();
    }
}
