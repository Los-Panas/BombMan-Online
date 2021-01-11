using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public enum PUTypes
{
    NONE,
    SPEED,
    BIG_BOMB,
    COOLDOWN
}

public class PowerUp
{
    public float lifeTime = 1f;
    public float lifeTimer = 0f;
    public PUTypes type = PUTypes.NONE;
    public bool blink = false;
    public float blinkingTimer = 0f;
    public PowerUp(float time, PUTypes type)
    {
        this.lifeTime = time;
        this.type = type;
        lifeTimer = Time.time;
        blinkingTimer = Time.time;
    }
}

public class BuffsManager : MonoBehaviour
{
    public float secondsUntilBlinking = 0f;
    public Image[] speedText;
    public Image[] bombText;
    public Image[] cooldownText;
    List<PowerUp> powerups = new List<PowerUp>();
    [HideInInspector]
    public MovementInput movement;
    [HideInInspector]
    public bool isFaster = false;
    [HideInInspector]
    public bool isBigBomb = false;
    [HideInInspector]
    public float initVelocity;

    PhotonView pv;
    private int indexPlayer = 50;
    Color[] playerColor;
    // Start is called before the first frame update
    void Start()
    {
        speedText = new Image[4];
        bombText = new Image[4];
        cooldownText = new Image[4];
        playerColor = new Color[4];
        movement = GetComponent<MovementInput>();
        initVelocity = movement.Velocity;
        SetCorrectUI();
        pv = GetComponent<PhotonView>();
    }

    private void SetCorrectUI()
    {
        GameSetUpController gameSetUp = GameObject.Find("GameSetUp").GetComponent<GameSetUpController>();
        Text[] names = gameSetUp.GetPlayernames();
        for(int i = 0; i < names.Length; i++)
        {
                indexPlayer = i;
            switch (indexPlayer)
            {
                case 0: //Red
                    {
                        GameObject gO = GameObject.Find("SpeedPURed");
                        speedText[i] = gO.GetComponent<Image>();
                        GameObject gO1 = GameObject.Find("BombPURed");
                        bombText[i] = gO1.GetComponent<Image>();
                        GameObject gO2 = GameObject.Find("CooldownPURed");
                        cooldownText[i] = gO2.GetComponent<Image>();
                        playerColor[i] = Color.red;
                    }
                    break;
                case 1://Yellow
                    {
                        GameObject gO = GameObject.Find("SpeedPUYellow");
                        speedText[i] = gO.GetComponent<Image>();
                        GameObject gO1 = GameObject.Find("BombPUYellow");
                        bombText[i] = gO1.GetComponent<Image>();
                        GameObject gO2 = GameObject.Find("CooldownPUYellow");
                        cooldownText[i] = gO2.GetComponent<Image>();
                        playerColor[i] = Color.yellow;
                    }
                    break;
                case 2://Blue
                    {
                        GameObject gO = GameObject.Find("SpeedPUBlue");
                        speedText[i] = gO.GetComponent<Image>();
                        GameObject gO1 = GameObject.Find("BombPUBlue");
                        bombText[i] = gO1.GetComponent<Image>();
                        GameObject gO2 = GameObject.Find("CooldownPUBlue");
                        cooldownText[i] = gO2.GetComponent<Image>();
                        playerColor[i] = Color.blue;
                    }
                    break;
                case 3://Black
                    {
                        GameObject gO = GameObject.Find("SpeedPUBlack");
                        speedText[i] = gO.GetComponent<Image>();
                        GameObject gO1 = GameObject.Find("BombPUBlack");
                        bombText[i] = gO1.GetComponent<Image>();
                        GameObject gO2 = GameObject.Find("CooldownPUBlack");
                        cooldownText[i] = gO2.GetComponent<Image>();
                        playerColor[i] = Color.black;
                    }
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < powerups.Count; ++i)
        {
            if (Time.time - powerups[i].lifeTimer > powerups[i].lifeTime)
            {
                SetValueToInitial(powerups[i]);
                powerups.Remove(powerups[i]);
                continue;
            }
            if(Time.time - powerups[i].lifeTimer > secondsUntilBlinking)
            {
                //Start blinking UI
                //if(Time.time - powerups[i].blinkingTimer > 0.2)
                //{
                //    powerups[i].blinkingTimer = Time.time;
                //    powerups[i].blink = !powerups[i].blink;
                //    switch(powerups[i].type)
                //    {
                //        case PUTypes.SPEED:
                //            speedText.gameObject.SetActive(powerups[i].blink);
                //            break;
                //        case PUTypes.BIG_BOMB:
                //            bombText.gameObject.SetActive(powerups[i].blink);
                //            break;
                //        case PUTypes.COOLDOWN:
                //            cooldownText.gameObject.SetActive(powerups[i].blink);
                //            break;
                //    }
                //    Debug.Log("BLINKING");
                //}
            }
        }
    }
    public void SetValueToInitial(PowerUp po)
    {
        Text[] names = GameObject.Find("GameSetUp").GetComponent<GameSetUpController>().GetPlayernames();
        for (int i = 0; i < names.Length; i++)
        {
            switch (po.type)
            {
                case PUTypes.NONE:

                    break;
                case PUTypes.SPEED:
                    movement.Velocity = initVelocity;
                    speedText[i].gameObject.SetActive(true);
                    speedText[i].color = Color.grey;              
                    isFaster = false;
                    break;
                case PUTypes.BIG_BOMB:
                    isBigBomb = false;
                    bombText[i].gameObject.SetActive(true);
                    bombText[i].color = Color.grey;
                    break;
                case PUTypes.COOLDOWN:
                    movement.bombCooldown = 3.0f;
                    cooldownText[i].gameObject.SetActive(true);
                    cooldownText[i].color = Color.grey;
                    break;
                default:
                    break;
            }
        }
    }

    public void AddPowerUp(PUTypes pType, float lifeTime, string na)
    {
        PowerUp newPU = new PowerUp(lifeTime, pType);
        powerups.Add(newPU);
        int aux = GameSetUpController.GS.GetPosition(na);
        switch (pType)
        {
            case PUTypes.NONE:

                break;
            case PUTypes.SPEED:
                movement.Velocity = 20;
                speedText[aux].color = playerColor[aux];
                break;
            case PUTypes.BIG_BOMB:
                isBigBomb = true;
                bombText[aux].color = playerColor[aux];
                break;
            case PUTypes.COOLDOWN:
                movement.bombCooldown = 1.0f;
                cooldownText[aux].color = playerColor[aux];
                break;
            default:
                break;
        }
    }

    public bool AlreadyHaveThisBuff(PUTypes po)
    {
        for (int i = 0; i < powerups.Count; ++i)
        {
            if (powerups[i].type == po)
            {
                powerups[i].lifeTimer = Time.realtimeSinceStartup;
                return true;
            }
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (string.Compare(other.gameObject.tag, "PowerUp") == 0)
        {
            PowerUpEntity po = other.gameObject.GetComponent<PowerUpEntity>();
            if (!AlreadyHaveThisBuff(po.type))
                pv.RPC("RCP_AddPowerUp", RpcTarget.All, new object[] { (int)po.type, po.lifeTime, PhotonNetwork.NickName});

            PowerUpSpawner spawner = GameObject.Find("PowerUpSpawner").GetComponent<PowerUpSpawner>();

            for (int i = 0; i < spawner.spawnedPowerUps.Count; ++i)
            {
                if (spawner.spawnedPowerUps[i] == other.gameObject)
                {
                    spawner.spawnedPowerUps.Remove(spawner.spawnedPowerUps[i]);
                    break;
                }
            }
            pv.RPC("RCP_DestroyPowerUp", RpcTarget.All, other.gameObject.GetComponent<PhotonView>().ViewID);

            
           
        }
    }

    [PunRPC]
    void RCP_DestroyPowerUp(int nID)
    {
        PhotonView aux = PhotonView.Find(nID);
        if(aux != null && aux.IsMine)
            PhotonNetwork.Destroy(aux);
    }

    [PunRPC]
    void RCP_AddPowerUp(int type,float lifeTime, string vID)
    {
        AddPowerUp((PUTypes)type, lifeTime, vID);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayAudioWithName("pickup");
    }
}
