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

    public void StartGame()
    {
        PV.RPC("RCP_StartGame", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RCP_StartGame()
    {
       GameSetUpController.GS.DisableCanvas();
       int id = GameSetUpController.GS.GetPosition() + 1;
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
        PV.RPC("RCP_DestroyAvatar", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    void RCP_DestroyAvatar()
    {
        if (PV.IsMine)
            PhotonNetwork.Destroy(myAvatar);
    }
}
