using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFuncs : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PanelBG;
    public GameObject RoomPanel;
    private string roomHost = "";
    private string existingRoom = "";
 

    public void OpenGamePanel()
    {
        PanelBG.SetActive(false);
        RoomPanel.SetActive(true);  
    }
    public void OpenMainPanel()
    {
        PanelBG.SetActive(true);
        RoomPanel.SetActive(false);
    }

    public void SetRoomName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Code Name is null or empty");
            return;
        }
        roomHost = value;
    }
    public void SetExistingRoomName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Room Name is null or empty");
            return;
        }
        existingRoom = value;
    }

    public void EnterHostRoom()
    {
        if (!string.IsNullOrEmpty(roomHost))
        {
            GameObject.Find("LobbyController").GetComponent<LobbyController>().CreateRoomWithTag(roomHost);
        }
    }
    public void EnterPrivateRoom()
    {

    }
}
