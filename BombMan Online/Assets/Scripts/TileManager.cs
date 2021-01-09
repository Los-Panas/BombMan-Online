using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;

    struct CubeColors
    {
        public int redCubes;
        public int yellowCubes;
        public int blueCubes;
        public int blackCubes;
    }

    [HideInInspector]
    public List<FloorCube> cubes = new List<FloorCube>();
    CubeColors cubeColors;

    private void Awake()
    {
        instance = this;
    }

    void InitializeCubeStruct()
    {
        cubeColors.redCubes = 0;
        cubeColors.yellowCubes = 0;
        cubeColors.blueCubes = 0;
        cubeColors.blackCubes = 0;
    }

    public void UpdateValues()
    {
        InitializeCubeStruct();

        for (int i = 0; i < cubes.Count; ++i)
        {
            switch (cubes[i].currentColor)
            {
                case CharacterSkinController.RobotColor.Red:
                    ++cubeColors.redCubes;
                    break;
                case CharacterSkinController.RobotColor.Yellow:
                    ++cubeColors.yellowCubes;
                    break;
                case CharacterSkinController.RobotColor.Blue:
                    ++cubeColors.blueCubes;
                    break;
                case CharacterSkinController.RobotColor.Black:
                    ++cubeColors.blackCubes;
                    break;
            }
        }

        // TODO: Update UI
    }
}
