using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCube : MonoBehaviour
{
    public enum Style
    {
        Dark,
        Light
    }


    Material mat;
    
    public Style cubeStyle = Style.Light;

    [HideInInspector]
    public CharacterSkinController.RobotColor currentColor = CharacterSkinController.RobotColor.None;

    private void Awake()
    {
        TileManager.instance.cubes.Add(this);
    }

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

        if (cubeStyle == Style.Dark)
        {
            mat.color = Color.white * 0.75f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BombPaint"))
        {
            Bomb bomb = other.transform.parent.parent.GetComponent<Bomb>();
            currentColor = bomb.bomb_color;
            Color color = bomb.color;

            if (cubeStyle == Style.Dark)
            {
                color *= 0.75f;
            }

            mat.color = color;
        }
    }
}
