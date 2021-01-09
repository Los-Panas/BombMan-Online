using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PUTypes
{
    NONE,
    SPEED,
    BIG_BOMB
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
    Text speedText;
    List<PowerUp> powerups = new List<PowerUp>();
    MovementInput movement;
    bool isFaster = false;
    bool isBigBomb = false;
    float initVelocity;
    float blinkingTimer = 0f;
    bool blink;
    // Start is called before the first frame update
    void Start()
    {
       movement = GetComponent<MovementInput>();
       initVelocity = movement.Velocity;
       GameObject gO = GameObject.Find("SPEEDUI");
        speedText = gO.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < powerups.Count; ++i)
        {
            if (Time.realtimeSinceStartup - powerups[i].lifeTimer > powerups[i].lifeTime)
            {
                SetValueToInitial(powerups[i]);
                powerups.Remove(powerups[i]);
                continue;
            }
            if(Time.realtimeSinceStartup - powerups[i].lifeTimer > secondsUntilBlinking)
            {
                //Start blinking UI
                if(Time.realtimeSinceStartup - blinkingTimer > 0.2)
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
                //Victor your shiet here
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
                //Victor your shiet here
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
            Destroy(other.gameObject);
        }
    }
}
