using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanMap : MonoBehaviour
{
    public static CleanMap CM;

    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        if(CM == null)
        {
            CM = this;
        }
        color = Color.white;
    }

   public void CleanAllMap()
    {
        MeshRenderer[] cubes = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer c in cubes)
        {
            c.material.color = color;
            if (color == Color.white)
                color.r = color.g = color.b = 0.8f;
            else
                color = Color.white;
        }
    }
}
