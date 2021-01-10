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
        if (PV.IsMine)
        {
            int id = GameSetUpController.GS.GetPosition() + 1;
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player" + id.ToString()),
            GameSetUpController.GS.spawnPoints[id].position, GameSetUpController.GS.spawnPoints[id].rotation);
            GameTimer.GT.NewGame();
        }
    }

    [PunRPC]
    void RCP_UpdateLobby(string name)
    {
        GameSetUpController.GS.PlayerConnected(name);
    }

    public void DestroyAvatar()
    {
        PhotonNetwork.Destroy(myAvatar);
    }

    [PunRPC]
    public void RPC_DestroyAvatar()
    {
        PhotonView newPV = myAvatar.GetComponent<PhotonView>();
        if(newPV != null && newPV.IsMine)
            PhotonNetwork.Destroy(newPV.gameObject);
       myAvatar = null;
    }

    private void OnDestroy()
    {
        DestroyAvatar();
    }
}
