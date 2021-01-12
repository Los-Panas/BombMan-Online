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
    public GameObject itsOverPanel;
    public Image fadeImage;

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
            start = false;
            powerupspawner.start = false;
            StartWinTransition();
        }
        else if (start)
        {
            double timeseg = timeToFinish - Time.time;
            int fseconds = Mathf.FloorToInt((float)timeseg);
            string s_seconds = fseconds.ToString();
            
            if (fseconds < 10)
            {
                s_seconds = "0" + s_seconds;
            }

            seconds.text = s_seconds;

            float fmiliseconds = Mathf.Round((float)System.Math.Round((timeseg - fseconds) * 100, 2));
            string s_miliseconds = fmiliseconds.ToString();

            if (fmiliseconds < 10)
            {
                s_miliseconds = "0" + s_miliseconds;
            }

            miliseconds.text = s_miliseconds;
        }
    }

    public void NewGame()
    {
        timeToFinish = totalGameTime + Time.time;
        start = true;
        powerupspawner.start = true;
    }

    void StartWinTransition()
    {
        itsOverPanel.SetActive(true);
    }

    public void PositionDone()
    {
        Invoke("StartFade", 2.0f);
    }

    void StartFade()
    {
        StartCoroutine(FadeImage());
    }

    IEnumerator FadeImage()
    {
        Color c = fadeImage.color;
        float time_start = Time.time;

        while (fadeImage.color.a != 1)
        {
            float t = (Time.time - time_start) / 0.5f;

            if (t < 1)
            {
                c.a = t;
            }
            else
            {
                c.a = 1;
            }

            fadeImage.color = c;

            yield return null;
        }

        itsOverPanel.SetActive(false);
        GameSetUpController.GS.DestroyAvatar();
        powerupspawner.DestroyCurrentPUs();
        WinLoseMenu.instance.Initialize();
    }

}
