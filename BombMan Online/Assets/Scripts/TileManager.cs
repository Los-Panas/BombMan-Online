using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    Text[] cubesText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Debug.Log(cubes.Count);
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
                case CharacterSkinController.RobotColor.None:
                    break;
            }
        }

        cubesText[0].text = cubeColors.redCubes.ToString();
        cubesText[1].text = cubeColors.yellowCubes.ToString();
        cubesText[2].text = cubeColors.blueCubes.ToString();
        cubesText[3].text = cubeColors.blackCubes.ToString();
    }
}
