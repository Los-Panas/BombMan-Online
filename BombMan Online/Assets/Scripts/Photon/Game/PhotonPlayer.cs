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
            PV.RPC("RCP_UpdateLobby", RpcTarget.AllBuffered, PhotonNetwork.NickName);
            GameSetUpController.GS.currentObject = gameObject;
        }
    }

    public void StartGame()
    {
        PV.RPC("RCP_StartGame", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RCP_StartGame()
    {
       GameSetUpController.GS.DisableCanvas();
       int id = GameSetUpController.GS.GetPosition();
       myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"),
       GameSetUpController.GS.spawnPoints[id].position, GameSetUpController.GS.spawnPoints[id].rotation); 
        
    }

    [PunRPC]
    void RCP_UpdateLobby(string name)
    {
        GameSetUpController.GS.PlayerConnected(name);
    }
}
