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

    private void Update()
    {
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
        if (playerNameIndex > -1 && startButton.gameObject.activeSelf == false
            && PhotonNetwork.IsMasterClient)
            startButton.gameObject.SetActive(true);

        playerNameIndex++;
    }

    public int GetPosition(string name)
    {
        int pos = 0;
        foreach(Text n in playerName)
        {
            if(n.text == name)
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
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.CurrentRoom.IsOpen = true;

    }

    public void ReturnToLobby()
    {
        myNetworkPlayer.GetComponent<PhotonPlayer>().ReturnToLobby();
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
    }

    public void DisconnectPlayer(string name)
    {
        int id = GetPosition(name);
        for (; id < playerName.Length - 1; ++id)
        {
            playerName[id].gameObject.SetActive(false);
            playerImage[id].SetActive(false);
            playerName[id].text = "";

            if (playerName[id + 1].text != "")
            {
                playerName[id].text = playerName[id + 1].text;
                playerNameIndex = id + 1;
            }
        }
    }


}
