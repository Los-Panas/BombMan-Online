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
            Color color = other.transform.parent.parent.GetComponent<Bomb>().color;
            if (cubeStyle == Style.Dark)
            {
                color *= 0.75f;
            }

            mat.color = color;
        }
    }
}
