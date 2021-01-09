using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class GameTimer : MonoBehaviour
{
    public static GameTimer GT;

    [SerializeField]
    private float totalGameTime;
    private float timeToFinish;
    // Start is called before the first frame update
    void OnEnable()
    {
        if(GT == null)
        {
            GT = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(timeToFinish - Time.time <= 0)
        {
            GetComponent<Text>().text = "0'00 s";
        }
        else
        {
            double timeseg = timeToFinish - Time.time;
            timeseg = System.Math.Round(timeseg, 2);
            GetComponent<Text>().text = timeseg.ToString() + " s";
        }
    }
    public void NewGame()
    {
        timeToFinish = totalGameTime + Time.time;
    }
}
