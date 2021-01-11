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
    [SerializeField]
    Image fadeImage;

    List<int> playersThatWon = new List<int>();

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void StartTransition()
    {
        StartCoroutine(FadeImage());
    }

    IEnumerator FadeImage()
    {
        Color c = fadeImage.color;
        float time_start = Time.time;

        while (fadeImage.color.a != 0)
        {
            float t = (Time.time - time_start) / 0.5f;

            if (t < 1)
            {
                c.a = 1 - t;
            }
            else
            {
                c.a = 0;
            }

            fadeImage.color = c;

            yield return null;
        }

        Invoke("ShowButton", 3.0f);
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        MainCamera.SetActive(false);
        TileManagerCanvas.SetActive(false);
        Color c = GameTimer.GT.fadeImage.color;
        c.a = 0;
        GameTimer.GT.fadeImage.color = c;
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
                pos = players[0].transform.localPosition;
                pos.x = 1;
                pos.z = 0;
                players[0].transform.localPosition = pos;

                pos = players[1].transform.localPosition;
                pos.x = -1;
                pos.z = 0;
                players[1].transform.localPosition = pos;
                break;
            case 3:
                pos = players[0].transform.localPosition;
                pos.x = 2;
                pos.z = 0;
                players[0].transform.localPosition = pos;

                pos = players[2].transform.localPosition;
                pos.x = -2;
                pos.z = 0;
                players[2].transform.localPosition = pos;
                break;
            case 4:
                pos = players[0].transform.localPosition;
                pos.x = 3;
                pos.z = 0;
                players[0].transform.localPosition = pos;

                pos = players[1].transform.localPosition;
                pos.x = 1;
                pos.z = 0;
                players[1].transform.localPosition = pos;

                pos = players[2].transform.localPosition;
                pos.x = -1;
                pos.z = 0;
                players[2].transform.localPosition = pos;

                pos = players[3].transform.localPosition;
                pos.x = -3;
                pos.z = 0;
                players[3].transform.localPosition = pos;
                break;
        }

        // ------------- WHO WON ----------------------------------------------

        if (cubecolors.redCubes < cubecolors.yellowCubes)
        {
            playersThatWon.Add(1);
        }
        else if(cubecolors.redCubes == cubecolors.yellowCubes)
        {
            playersThatWon.Add(0);
            playersThatWon.Add(1);
        }
        else
        {
            playersThatWon.Add(0);
        }

        if (playersNum > 2)
        {
            for (int i = 0; i < playersThatWon.Count; ++i)
            {
                switch (playersThatWon[i])
                {
                    case 0:
                        if (cubecolors.redCubes < cubecolors.blueCubes)
                        {
                            playersThatWon.Clear();
                            playersThatWon.Add(2);
                            goto exit;
                        }
                        else if (cubecolors.redCubes == cubecolors.blueCubes)
                        {
                            playersThatWon.Add(2);
                            goto exit;
                        }
                        break;
                    case 1:
                        if (cubecolors.yellowCubes < cubecolors.blueCubes)
                        {
                            playersThatWon.Clear();
                            playersThatWon.Add(2);
                            goto exit;
                        }
                        else if (cubecolors.yellowCubes == cubecolors.blueCubes)
                        {
                            playersThatWon.Add(2);
                            goto exit;
                        }
                        break;
                }
            }

            exit:;

            if (playersNum > 3)
            {
                for (int i = 0; i < playersThatWon.Count; ++i)
                {
                    switch (playersThatWon[i])
                    {
                        case 0:
                            if (cubecolors.redCubes < cubecolors.blackCubes)
                            {
                                playersThatWon.Clear();
                                playersThatWon.Add(3);
                                goto exit2;
                            }
                            else if (cubecolors.redCubes == cubecolors.blackCubes)
                            {
                                playersThatWon.Add(3);
                                goto exit2;
                            }
                            break;
                        case 1:
                            if (cubecolors.yellowCubes < cubecolors.blackCubes)
                            {
                                playersThatWon.Clear();
                                playersThatWon.Add(3);
                                goto exit2;
                            }
                            else if (cubecolors.yellowCubes == cubecolors.blackCubes)
                            {
                                playersThatWon.Add(3);
                                goto exit2;
                            }
                            break;
                        case 2:
                            if (cubecolors.blueCubes < cubecolors.blackCubes)
                            {
                                playersThatWon.Clear();
                                playersThatWon.Add(3);
                                goto exit2;
                            }
                            else if (cubecolors.blueCubes == cubecolors.blackCubes)
                            {
                                playersThatWon.Add(3);
                                goto exit2;
                            }
                            break;
                    }
                }
            }

        exit2:;
        }

        // --------------------------------------------------------------------------

        if (playersThatWon.Count > 1)
        {
            playerwonText.text = "It's a Draw!";
        }
        else
        {
            playerwonText.text = GameSetUpController.GS.GetPlayernames()[playersThatWon[0]].text + " won!";
            for (int i = 0; i < materials.Length; ++i)
            {
                materials[i].material.color = colors[playersThatWon[0]];
            }
        }

        // Set Players UI
        switch (playersNum)
        {
            case 2:
                pos = new Vector2(-170, -220);
                playerNames[0].GetComponent<RectTransform>().anchoredPosition = pos;
                pos = new Vector2(170, -220);
                playerNames[1].GetComponent<RectTransform>().anchoredPosition = pos;
                break;
            case 3:
                pos = new Vector2(-340, -220);
                playerNames[0].GetComponent<RectTransform>().anchoredPosition = pos;
                pos = new Vector2(0, -220);
                playerNames[1].GetComponent<RectTransform>().anchoredPosition = pos;
                pos = new Vector2(340, -220);
                playerNames[2].GetComponent<RectTransform>().anchoredPosition = pos;
                break;
            case 4:
                pos = new Vector2(-510, -220);
                playerNames[0].GetComponent<RectTransform>().anchoredPosition = pos;
                pos = new Vector2(-170, -220);
                playerNames[1].GetComponent<RectTransform>().anchoredPosition = pos;
                pos = new Vector2(170, -220);
                playerNames[2].GetComponent<RectTransform>().anchoredPosition = pos;
                pos = new Vector2(510, -220);
                playerNames[3].GetComponent<RectTransform>().anchoredPosition = pos;
                break;
        }


        // Set Winners UI
        switch (playersNum)
        {
            case 2:
                for (int i = 0; i < playersThatWon.Count; ++i)
                {
                    switch (playersThatWon[i])
                    {
                        case 0:
                            pos = new Vector2(-200, -290);
                            playerNames[0].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                        case 1:
                            pos = new Vector2(200, -290);
                            playerNames[1].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < playersThatWon.Count; ++i)
                {
                    switch (playersThatWon[i])
                    {
                        case 0:
                            pos = new Vector2(-420, -290);
                            playerNames[0].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                        case 1:
                            pos = new Vector2(0, -290);
                            playerNames[1].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                        case 2:
                            pos = new Vector2(420, -290);
                            playerNames[2].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                    }

                }
                break;
            case 4:
                for (int i = 0; i < playersThatWon.Count; ++i)
                {
                    switch (playersThatWon[i])
                    {
                        case 0:
                            pos = new Vector2(-620, -290);
                            playerNames[0].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                        case 1:
                            pos = new Vector2(-200, -290);
                            playerNames[1].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                        case 2:
                            pos = new Vector2(200, -290);
                            playerNames[2].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                        case 3:
                            pos = new Vector2(620, -290);
                            playerNames[3].GetComponent<RectTransform>().anchoredPosition = pos;
                            break;
                    }
                }
                break;
        }

        for (int i = 0; i < playersNum; ++i)
        {
            for (int j = 0; j < playersThatWon.Count; ++j)
            {
                if (i == playersThatWon[j])
                {
                    pos = players[i].transform.localPosition;
                    pos.z = 1;
                    players[i].transform.localPosition = pos;
                    players[i].SetAnimationWinLose("happy");
                    playerLights[i].gameObject.SetActive(true);

                    if (playersThatWon.Count == 1)
                    {
                        playerNames[i].color = Color.white;
                        percentages[i].color = Color.white;
                    }

                    goto skipNotWinner;
                }
            }


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
            

        skipNotWinner:;

            Vector3 dir = cam.transform.position - players[i].transform.position;
            dir.y = players[i].transform.position.y;
            players[i].transform.rotation = Quaternion.LookRotation(dir);

            playerNames[i].text = GameSetUpController.GS.GetPlayernames()[i].text;

            switch (i)
            {
                case 0:
                    percentages[i].text = (Mathf.Round(((float)cubecolors.redCubes / 133.0f) * 100.0f * 100.0f) / 100.0f).ToString();
                    break;
                case 1:
                    percentages[i].text = (Mathf.Round(((float)cubecolors.yellowCubes / 133.0f) * 100.0f * 100.0f) / 100.0f).ToString();
                    break;
                case 2:
                    percentages[i].text = (Mathf.Round(((float)cubecolors.blueCubes / 133.0f) * 100.0f * 100.0f) / 100.0f).ToString();
                    break;
                case 3:
                    percentages[i].text = (Mathf.Round(((float)cubecolors.blackCubes / 133.0f) * 100.0f * 100.0f) / 100.0f).ToString();
                    break;
            }
        }

        StartTransition();
    }

    public void ReturnToLobby()
    {
        for (int i = 0; i < materials.Length; ++i)
        {
            materials[i].material.color = Color.white;
        }

        playersThatWon.Clear();
        CleanMap.CM.CleanAllMap();
        TileManager.instance.UpdateValues();
        gameObject.SetActive(false);
        MainCamera.SetActive(true);
        TileManagerCanvas.SetActive(true);
        GeneralLight.SetActive(true);
        LobbyButton.SetActive(false);
        Color c = fadeImage.color;
        c.a = 1;
        fadeImage.color = c;
        GameTimer.GT.itsOverPanel.GetComponent<Animator>().SetTrigger("Again");
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
