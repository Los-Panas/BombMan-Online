using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class WinLoseMenu : MonoBehaviour
{
    static public WinLoseMenu instance; 

    [SerializeField]
    CharacterSkinController[] players;
    [SerializeField]
    Light[] playerLights;
    [SerializeField]
    MeshRenderer[] materials;
    [SerializeField]
    GameObject TileManagerCanvas;
    [SerializeField]
    GameObject cam;
    [SerializeField]
    Text playerwonText;
    [SerializeField]
    GameObject GeneralLight;
    [SerializeField]
    Color[] colors;
    [SerializeField]
    Text[] playerNames;
    [SerializeField]
    Text[] percentages;
    [SerializeField]
    GameObject MainCamera;
    [SerializeField]
    GameObject LobbyButton;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        MainCamera.SetActive(false);
        TileManagerCanvas.SetActive(false);
        GeneralLight.SetActive(false);

        TileManager.CubeColors cubecolors = TileManager.instance.cubeColors;

        int playersNum = PhotonNetwork.CurrentRoom.PlayerCount;

        for (int i = 0; i < playersNum; ++i)
        {
            players[i].gameObject.SetActive(true);
            playerNames[i].gameObject.SetActive(true);
            players[i].Initialize();
        }

        Vector3 pos;

        switch (playersNum)
        {
            case 2:
                pos = players[0].transform.position;
                pos.x += 1;
                players[0].transform.position = pos;

                pos = players[1].transform.position;
                pos.x -= 1;
                players[1].transform.position = pos;
                break;
            case 3:
                pos = players[0].transform.position;
                pos.x += 2;
                players[0].transform.position = pos;

                pos = players[2].transform.position;
                pos.x -= 2;
                players[2].transform.position = pos;
                break;
            case 4:
                pos = players[0].transform.position;
                pos.x += 3;
                players[0].transform.position = pos;

                pos = players[1].transform.position;
                pos.x += 1;
                players[1].transform.position = pos;

                pos = players[2].transform.position;
                pos.x -= 1;
                players[2].transform.position = pos;

                pos = players[3].transform.position;
                pos.x -= 3;
                players[3].transform.position = pos;
                break;
        }

        // ------------- WHO WON ----------------------------------------------
        int playerWon = 0;

        if (cubecolors.redCubes < cubecolors.yellowCubes)
        {
            playerWon = 1;
        }

        if (playersNum > 2)
        {
            switch (playerWon)
            {
                case 0:
                    if (cubecolors.redCubes < cubecolors.blueCubes)
                    {
                        playerWon = 2;
                    }
                    break;
                case 1:
                    if (cubecolors.yellowCubes < cubecolors.blueCubes)
                    {
                        playerWon = 2;
                    }
                    break;
            }

            if (playersNum > 3)
            {
                switch (playerWon)
                {
                    case 0:
                        if (cubecolors.redCubes < cubecolors.blackCubes)
                        {
                            playerWon = 3;
                        }
                        break;
                    case 1:
                        if (cubecolors.yellowCubes < cubecolors.blackCubes)
                        {
                            playerWon = 3;
                        }
                        break;
                    case 2:
                        if (cubecolors.blueCubes < cubecolors.blackCubes)
                        {
                            playerWon = 3;
                        }
                        break;
                }
            }
        }
        // --------------------------------------------------------------------------

        for (int i = 0; i < materials.Length; ++i)
        {
            materials[i].material.color = colors[playerWon];
        }

        playerwonText.text = GameSetUpController.GS.GetPlayernames()[playerWon].text + " won!";

        switch (playersNum)
        {
            case 2:
                switch (playerWon)
                {
                    case 0:
                        pos = new Vector2(-200, -290);
                        playerNames[0].GetComponent<RectTransform>().anchoredPosition = pos;

                        pos = new Vector2(170, -220);
                        playerNames[1].GetComponent<RectTransform>().anchoredPosition = pos;
                        break;
                    case 1:
                        pos = new Vector2(-170, -220);
                        playerNames[0].GetComponent<RectTransform>().anchoredPosition = pos;

                        pos = new Vector2(200, -290);
                        playerNames[1].GetComponent<RectTransform>().anchoredPosition = pos;
                        break;
                }
                break;
            case 3:
                for (int i = 0; i < 3; ++i)
                {
                    switch (i)
                    {
                        case 0:
                            pos = new Vector2(-340, -220);
                            playerNames[i].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                        case 1:
                            pos = new Vector2(0, -220);
                            playerNames[i].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                        case 2:
                            pos = new Vector2(340, -220);
                            playerNames[i].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                    }
                }

                switch (playerWon)
                {
                    case 0:
                        pos = new Vector2(-420, -290);
                        playerNames[playerWon].GetComponent<RectTransform>().anchoredPosition = pos;
                        break;
                    case 1:
                        pos = new Vector2(0, -290);
                        playerNames[playerWon].GetComponent<RectTransform>().anchoredPosition = pos;
                        break;
                    case 2:
                        pos = new Vector2(420, -290);
                        playerNames[playerWon].GetComponent<RectTransform>().anchoredPosition = pos;
                        break;
                }
                break;
            case 4:
                for (int i = 0; i < 4; ++i)
                {
                    switch (i)
                    {
                        case 0:
                            pos = new Vector2(-510, -220);
                            playerNames[i].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                        case 1:
                            pos = new Vector2(-170, -220);
                            playerNames[i].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                        case 2:
                            pos = new Vector2(170, -220);
                            playerNames[i].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                        case 3:
                            pos = new Vector2(510, -220);
                            playerNames[i].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                    }
                }

                switch (playerWon)
                {
                    case 0:
                        pos = new Vector2(-620, -290);
                        playerNames[playerWon].GetComponent<RectTransform>().anchoredPosition = pos;
                        break;
                    case 1:
                        pos = new Vector2(-200, -290);
                        playerNames[playerWon].GetComponent<RectTransform>().anchoredPosition = pos;
                        break;
                    case 2:
                        pos = new Vector2(200, -290);
                        playerNames[playerWon].GetComponent<RectTransform>().anchoredPosition = pos;
                        break;
                    case 3:
                        pos = new Vector2(620, -290);
                        playerNames[playerWon].GetComponent<RectTransform>().anchoredPosition = pos;
                        break;
                }
                break;
        }

        for (int i = 0; i < playersNum; ++i)
        {
            if (i == playerWon)
            {
                pos = players[i].transform.position;
                pos.z += 1;
                players[i].transform.position = pos;
                players[i].SetAnimationWinLose("happy");
                playerLights[i].gameObject.SetActive(true);
                playerNames[i].color = Color.white;
                percentages[i].color = Color.white;
            }
            else
            {
                int random = Random.Range(0, 2);
                if (random == 0)
                {
                    players[i].SetAnimationWinLose("angry");
                }
                else
                {
                    players[i].SetAnimationWinLose("dead");
                }

                playerLights[i].gameObject.SetActive(false);
                playerNames[i].color = colors[i];
                percentages[i].color = colors[i];
            }

            Vector3 dir = cam.transform.position - players[i].transform.position;
            dir.y = players[i].transform.position.y;
            players[i].transform.rotation = Quaternion.LookRotation(dir);

            playerNames[i].text = GameSetUpController.GS.GetPlayernames()[i].text;

            switch (i)
            {
                case 0:
                    percentages[i].text = (System.Math.Round(((float)cubecolors.redCubes / 133.0f) * 100, 2)).ToString() + "%";
                    break;
                case 1:
                    percentages[i].text = (System.Math.Round(((float)cubecolors.yellowCubes / 133.0f) * 100, 2)).ToString() + "%";
                    break;
                case 2:
                    percentages[i].text = (System.Math.Round(((float)cubecolors.blueCubes / 133.0f) * 100, 2)).ToString() + "%";
                    break;
                case 3:
                    percentages[i].text = (System.Math.Round(((float)cubecolors.blackCubes / 133.0f) * 100, 2)).ToString() + "%";
                    break;
            }
        }

        Invoke("ShowButton", 3.0f);
    }

    public void ReturnToLobby()
    {
        gameObject.SetActive(false);
        MainCamera.SetActive(true);
        TileManagerCanvas.SetActive(true);
        GeneralLight.SetActive(true);
        LobbyButton.SetActive(false);
        GameSetUpController.GS.EnableCanvas();
    }

    void ShowButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            LobbyButton.SetActive(true);
        }
    }
}
