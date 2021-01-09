using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinController : MonoBehaviour
{
    public enum RobotColor
    {
        Red,
        Yellow,
        Blue,
        Black,

        None
    }

    Animator animator;
    Renderer[] characterMaterials;

    public RobotColor color = RobotColor.None;
    [HideInInspector]
    public Color childColor;
    [SerializeField]
    Material[] RobotColors;
    [ColorUsage(true,true)]
    public Color[] eyeColors;
    public enum EyePosition { normal, happy, angry, dead}
    public EyePosition eyeState;
    [SerializeField]
    SkinnedMeshRenderer[] affectedColorMeshes;

    // Start is called before the first frame update
    void Start()
    {
        SetColor(color);
    }

    public void SetColor(RobotColor col)
    {
        animator = GetComponent<Animator>();
        characterMaterials = GetComponentsInChildren<Renderer>();

        Material current_color = null;
        switch (col)
        {
            case RobotColor.Red:
                current_color = RobotColors[0];
                childColor = new Color(0.75f, 0, 0, 1);
                break;
            case RobotColor.Yellow:
                current_color = RobotColors[1];
                childColor = new Color(0.75f, 0.75f, 0.15f, 1);
                break;
            case RobotColor.Blue:
                current_color = RobotColors[2];
                childColor = new Color(0, 0.75f, 0.75f, 1);
                break;
            case RobotColor.Black:
                current_color = RobotColors[3];
                childColor = new Color(0.25f, 0.25f, 0.25f, 1);
                break;
        }

        for (int i = 0; i < affectedColorMeshes.Length; ++i)
        {
            affectedColorMeshes[i].material = current_color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //ChangeMaterialSettings(0);
            ChangeEyeOffset(EyePosition.normal);
            ChangeAnimatorIdle("normal");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //ChangeMaterialSettings(1);
            ChangeEyeOffset(EyePosition.angry);
            ChangeAnimatorIdle("angry");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //ChangeMaterialSettings(2);
            ChangeEyeOffset(EyePosition.happy);
            ChangeAnimatorIdle("happy");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //ChangeMaterialSettings(3);
            ChangeEyeOffset(EyePosition.dead);
            ChangeAnimatorIdle("dead");
        }
    }

    void ChangeAnimatorIdle(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    void ChangeEyeOffset(EyePosition pos)
    {
        Vector2 offset = Vector2.zero;

        switch (pos)
        {
            case EyePosition.normal:
                offset = new Vector2(0, 0);
                break;
            case EyePosition.happy:
                offset = new Vector2(.33f, 0);
                break;
            case EyePosition.angry:
                offset = new Vector2(.66f, 0);
                break;
            case EyePosition.dead:
                offset = new Vector2(.33f, .66f);
                break;
            default:
                break;
        }

        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetTextureOffset("_MainTex", offset);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("BombPaint"))
        //{
        //    // Fucking die
        //}
    }
}
