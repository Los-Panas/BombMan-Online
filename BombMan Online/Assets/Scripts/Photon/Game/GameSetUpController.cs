﻿using Photon.Pun;
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

    public GameObject currentObject;

    private void OnEnable()
    {
      if(GameSetUpController.GS == null)
        {
            GameSetUpController.GS = this;
        }
    }

    public void PlayerConnected(string name)
    {
        playerName[playerNameIndex].gameObject.SetActive(true);
        playerName[playerNameIndex].text = name;
        //if (playerNameIndex > 0 && startButton.gameObject.activeSelf == false
            //&& PhotonNetwork.IsMasterClient)
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
        if (currentObject != null)
            currentObject.GetComponent<PhotonPlayer>().StartGame();
    }

    public void DisableCanvas()
    {
        canvas.gameObject.SetActive(false);
    }
}