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
    public PowerUp(float time, PUTypes type)
    {
        this.lifeTime = time;
        this.type = type;
        lifeTimer = Time.realtimeSinceStartup;
    }
}

public class BuffsManager : MonoBehaviour
{
    public float secondsUntilBlinking = 0f;
    public Image speedText;
    public Image bombText;
    public Image cooldownText;
    List<PowerUp> powerups = new List<PowerUp>();
    MovementInput movement;
    bool isFaster = false;
    [HideInInspector]
    public bool isBigBomb = false;
    float initVelocity;
    float blinkingTimer = 0f;
    bool blink;
    PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
       movement = GetComponent<MovementInput>();
       initVelocity = movement.Velocity;
       SetCorrectUI();
        pv = GetComponent<PhotonView>();
    }

    private void SetCorrectUI()
    {
        GameSetUpController gameSetUp = GameObject.Find("GameSetUp").GetComponent<GameSetUpController>();
        Text[] names = gameSetUp.GetPlayernames();
        int indexPlayer = 50;
        for(int i = 0; i < names.Length; i++)
        {
            if(string.Compare(names[i].text, PhotonNetwork.NickName) == 0)
            {
                indexPlayer = i;
                break;
            }
        }
        Debug.Log(indexPlayer);
        switch(indexPlayer)
        {
            case 0: //Red
                {
                    GameObject gO = GameObject.Find("SpeedPURed");
                    speedText = gO.GetComponent<Image>();
                    GameObject gO1 = GameObject.Find("BombPURed");
                    bombText = gO1.GetComponent<Image>();
                    GameObject gO2 = GameObject.Find("CooldownPURed");
                    cooldownText = gO2.GetComponent<Image>();
                }
                break;
            case 1://Yellow
                {
                    GameObject gO = GameObject.Find("SpeedPUYellow");
                    speedText = gO.GetComponent<Image>();
                    GameObject gO1 = GameObject.Find("BombPUYellow");
                    bombText = gO1.GetComponent<Image>();
                    GameObject gO2 = GameObject.Find("CooldownPUYellow");
                    cooldownText = gO2.GetComponent<Image>();
                }
                break;
            case 2://Blue
                {
                    GameObject gO = GameObject.Find("SpeedPUBlue");
                    speedText = gO.GetComponent<Image>();
                    GameObject gO1 = GameObject.Find("BombPUBlue");
                    bombText = gO1.GetComponent<Image>();
                    GameObject gO2 = GameObject.Find("CooldownPUBlue");
                    cooldownText = gO2.GetComponent<Image>();
                }
                break;
            case 3://Black
                {
                    GameObject gO = GameObject.Find("SpeedPUBlack");
                    speedText = gO.GetComponent<Image>();
                    GameObject gO1 = GameObject.Find("BombPUBlack");
                    bombText = gO1.GetComponent<Image>();
                    GameObject gO2 = GameObject.Find("CooldownPUBlack");
                    cooldownText = gO2.GetComponent<Image>();
                }
                break;
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
                if(Time.time - blinkingTimer > 0.2)
                {
                    blinkingTimer = Time.realtimeSinceStartup;
                    blink = !blink;
                    speedText.gameObject.SetActive(blink);
                }
            }
        }
    }
    public void SetValueToInitial(PowerUp po)
    {
        switch (po.type)
        {
            case PUTypes.NONE:

                break;
            case PUTypes.SPEED:
                movement.Velocity = initVelocity;
                speedText.color = Color.grey;
                isFaster = false;
                break;
            case PUTypes.BIG_BOMB:
                isBigBomb = false;
                break;
            case PUTypes.COOLDOWN:
                movement.bombCooldown = 3.0f;
                break;
            default:
                break;
        }
    }

    public void AddPowerUp(PUTypes pType, float lifeTime)
    {
        PowerUp newPU = new PowerUp(lifeTime, pType);
        powerups.Add(newPU);
        switch (pType)
        {
            case PUTypes.NONE:

                break;
            case PUTypes.SPEED:
                movement.Velocity = 20;
                speedText.color = Color.white;
                break;
            case PUTypes.BIG_BOMB:
                isBigBomb = true;
                break;
            case PUTypes.COOLDOWN:
                movement.bombCooldown = 1.0f;
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
            if(!AlreadyHaveThisBuff(po.type))
                AddPowerUp(po.type, po.lifeTime);

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
}
