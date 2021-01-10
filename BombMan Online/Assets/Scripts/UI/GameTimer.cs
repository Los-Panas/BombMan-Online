using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class GameTimer : MonoBehaviour
{
    public static GameTimer GT;

    [SerializeField]
    Text seconds;
    [SerializeField]
    Text miliseconds;

    [SerializeField]
    private float totalGameTime;
    private float timeToFinish;
    PowerUpSpawner powerupspawner;



    bool start = false;
    // Start is called before the first frame update
    private void Start()
    {
        powerupspawner = GameObject.Find("PowerUpSpawner").GetComponent<PowerUpSpawner>();
    }
    void OnEnable()
    {
        if (GT == null)
        {
            GT = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToFinish - Time.time <= 0 && start)
        {
            seconds.text = "00";
            miliseconds.text = "00";
            //estroy(this); 
            //PhotonDestroyAvatar.DA.DestroyAvatar();
            GameSetUpController.GS.DestroyAvatar();
            start = false;
            powerupspawner.start = false;
            GameSetUpController.GS.EnableCanvas();
        }
        else if (start)
        {
            double timeseg = timeToFinish - Time.time;
            int fseconds = Mathf.FloorToInt((float)timeseg);
            seconds.text = fseconds.ToString();
            miliseconds.text = Mathf.Round((float)System.Math.Round((timeseg - fseconds) * 100, 2)).ToString();
        }
    }
    public void NewGame()
    {
        timeToFinish = totalGameTime + Time.time;
        start = true;
        powerupspawner.start = true;
        CleanMap.CM.CleanAllMap();
    }
}
