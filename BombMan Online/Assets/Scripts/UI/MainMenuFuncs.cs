using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFuncs : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PanelBG;
    public GameObject RoomPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
