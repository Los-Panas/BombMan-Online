using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static RoomController room;

    public int currentScene;
    private int multiplayerScene;

    private void Awake()
    {
        if (room == null)
        {
            room = this;
        }
        else
        {
            if (room != this)
            {
                Destroy(room.gameObject);
                room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        if (!CheckIfNameIsAlreadyTaken())
        {
            StartGame();

        }
        else
        {

            PhotonNetwork.LeaveRoom();

        }
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Disconnect();
        //Destroy(gameObject);
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    public void GoToMenu()
    {
        PhotonNetwork.LeaveRoom();
    }
    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(multiplayerScene);
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if(currentScene == multiplayerScene)
        {
            if (!CheckIfNameIsAlreadyTaken())
                CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPowerUpSpawner"), transform.position, Quaternion.identity, 0);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonWinLose"),new Vector3(8.02f,3.21f,-32.25f), Quaternion.identity, 0).SetActive(false);

    }

    public void SetLevelMap(int value)
    {
        multiplayerScene = value;
    }

    private bool CheckIfNameIsAlreadyTaken()
    {
        foreach (Player player in PhotonNetwork.PlayerListOthers)
        {
            if (player.NickName == PhotonNetwork.NickName)
            {
                Debug.LogError("Player name already taken");
                return true;
            }
        }

        return false;
    }
}
