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
        if (CM == null)
        {
            CM = this;
        }


        color = Color.white;
    }

   public void CleanAllMap()
    {
        var cubes = TileManager.instance.cubes;
        Color c = Color.white;

        for (int i = 0; i < cubes.Count; ++i)
        {
            cubes[i].currentColor = CharacterSkinController.RobotColor.None;
            if (cubes[i].cubeStyle == FloorCube.Style.Dark)
            {
                cubes[i].mat.color = c * 0.75f;
            }
            else
            {
                cubes[i].mat.color = c;
            }
        }
    }
}
