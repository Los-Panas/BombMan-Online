using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetUpController : MonoBehaviour
{
    public static GameSetUpController GS;

    public Transform[] spawnPoints;

    private void OnEnable()
    {
      if(GameSetUpController.GS == null)
        {
            GameSetUpController.GS = this;
        }
    }

}
