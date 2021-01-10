using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class WinLoseMenu : MonoBehaviour
{
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

    private void Start()
    {
        Camera.main.gameObject.SetActive(false);
        TileManagerCanvas.SetActive(false);
        GeneralLight.SetActive(false);

        TileManager.CubeColors cubecolors = TileManager.instance.cubeColors;
        //TODO: Change Positions
        int playersNum = PhotonNetwork.CurrentRoom.PlayerCount;

        for (int i = 0; i < playersNum; ++i)
        {
            players[i].gameObject.SetActive(true);
            players[i].Initialize();
        }

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

        for (int i = 0; i < materials.Length; ++i)
        {
            materials[i].material.color = colors[playerWon];
        }

        playerwonText.text = GameSetUpController.GS.GetPlayernames()[playerWon].text + "won!";

        for (int i = 0; i < playersNum; ++i)
        {
            if (i == playerWon)
            {
                Vector3 pos = players[i].transform.position;
                pos.z += 1;
                players[i].transform.position = pos;
                players[i].SetAnimationWinLose("happy");
                playerLights[i].gameObject.SetActive(true);
                playerNames[i].color = Color.white;
                percentages[i].color = Color.white;
                
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

            playerNames[i].text = GameSetUpController.GS.GetPlayernames()[playerWon].text;

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
    }
}
