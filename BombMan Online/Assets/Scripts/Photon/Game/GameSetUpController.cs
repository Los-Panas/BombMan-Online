using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameSetUpController : MonoBehaviour
{
    public static GameSetUpController GS;

    public Transform[] spawnPoints;

    [SerializeField]
    private Text[] playerName;
    private int playerNameIndex;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject[] playerImage;

    public GameObject myNetworkPlayer;


    private void OnEnable()
    {
      if(GameSetUpController.GS == null)
        {
            GameSetUpController.GS = this;
        }
    }

    public Text[] GetPlayernames()
    {
        return playerName;
    }

    public void PlayerConnected(string name)
    {
        playerName[playerNameIndex].gameObject.SetActive(true);
        playerImage[playerNameIndex].SetActive(true);
        playerName[playerNameIndex].text = name;
        if (playerNameIndex > 0 && startButton.gameObject.activeSelf == false
            && PhotonNetwork.IsMasterClient)
            startButton.gameObject.SetActive(true);

        playerNameIndex++;
    }

    public int GetPosition()
    {
        int pos = 0;
        foreach(Text n in playerName)
        {
            if(n.text == PhotonNetwork.NickName)
            {
                break;
            }
            ++pos;
        }
        return pos;
    }
    public void StartGame()
    {
        if (myNetworkPlayer != null)
            myNetworkPlayer.GetComponent<PhotonPlayer>().StartGame();
    }

    public void DisableCanvas()
    {
        canvas.gameObject.SetActive(false);
        GameTimer.GT.NewGame();
    }

    public void EnableCanvas()
    {
        canvas.gameObject.SetActive(true);

    }

    public void CloseServer()
    {
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    public void DestroyAvatar()
    {
        if (myNetworkPlayer != null)
            myNetworkPlayer.GetComponent<PhotonPlayer>().DestroyAvatar();

        //foreach (GameObject av in allAvatars)
        //{
        //   // av.GetComponent<PhotonPlayer>().DestroyAvatar();
        //}
        //allAvatars.Clear();
    }
}
