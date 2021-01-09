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

    bool start = false;
    // Start is called before the first frame update
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
        if(timeToFinish - Time.time <= 0 && start)
        {
                seconds.text = "00";
                miliseconds.text = "00";
                Destroy(this);
                if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.DestroyAll();
        }
        else if (start)
        {
            double timeseg = timeToFinish - Time.time;
            timeseg = System.Math.Round(timeseg, 2);
            int fseconds = Mathf.FloorToInt((float)timeseg);
            seconds.text = fseconds.ToString();
            miliseconds.text = (timeseg - fseconds).ToString();
        }
    }
    public void NewGame()
    {
        timeToFinish = totalGameTime + Time.time;
        start = true;
    }
}
