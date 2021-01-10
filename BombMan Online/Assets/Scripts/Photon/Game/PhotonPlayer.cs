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
            GameObject.Find("RoomController").GetComponent<RoomController>().GoToMenu();
            PhotonNetwork.Destroy(gameObject);

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
        
            int id = GameSetUpController.GS.GetPosition(PhotonNetwork.NickName) + 1;
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player" + id.ToString()),
            GameSetUpController.GS.spawnPoints[id].position, GameSetUpController.GS.spawnPoints[id].rotation);
       

    }

    [PunRPC]
    void RCP_UpdateLobby(string name)
    {
        GameSetUpController.GS.PlayerConnected(name);
    }

    public void DestroyAvatar()
    {
        PV.RPC("RPC_DestroyAvatar", RpcTarget.AllViaServer);
    }

    [PunRPC]
    public void RPC_DestroyAvatar()
    {
        if (myAvatar == null)
            return;
       PhotonView newPV = myAvatar.GetComponent<PhotonView>();
        if(newPV != null)
           PhotonNetwork.Destroy(newPV.gameObject);
      myAvatar = null;
    }

    [PunRPC]
    public void RPC_Disconnect(string name)
    {
        GameSetUpController.GS.DisconnectPlayer(name);
    }

    private void OnDestroy()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.DestroyAll();
        else
            PV.RPC("RPC_Disconnect", RpcTarget.AllBufferedViaServer, PhotonNetwork.NickName);
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
