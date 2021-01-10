using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class PowerUpSpawner : MonoBehaviour
{
    List<FloorCube> tilesMap;
    public List<GameObject> spawnedPowerUps;
    public int maxPowerUps = 5;
    int currentPU = 0;
    int currentPowerUps = 0;
    float spawnTime = 0f;
    float spawnTimer = 0f;
    public GameObject speedPU;
    public GameObject bombPU;
    public GameObject cooldownPU;
    int puType;
    PhotonView pv;
    [HideInInspector]
    public bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        tilesMap = TileManager.instance.cubes;
        spawnTime = UnityEngine.Random.Range(5f, 20f);
        spawnTimer = Time.time;
        puType = UnityEngine.Random.Range(0, 1);
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTimer > spawnTime && spawnedPowerUps.Count < maxPowerUps && start)
        {
            spawnTime = UnityEngine.Random.Range(5f, 10f);
            spawnTimer = Time.time;
            puType = UnityEngine.Random.Range(1, 4);
            Vector3 pos = SetRandomTilePosition();
            InstantiatePowerUp(puType, pos);
        }
    }

    private void InstantiatePowerUp(int puType, Vector3 pos)
    {
        Vector3 rot = new Vector3(-90, 0, 0);
        switch ((PUTypes)puType)
        {
            case PUTypes.SPEED:
                spawnedPowerUps.Add(PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PowerUpSpeed"), pos, Quaternion.Euler(rot)));//TODO: BIGBOMB
                break;
            case PUTypes.BIG_BOMB:
                spawnedPowerUps.Add(PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PowerUpBomb"), pos, Quaternion.Euler(rot)));//TODO: BIGBOMB
                break;
            case PUTypes.COOLDOWN:
                spawnedPowerUps.Add(PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PowerUpCooldown"), pos, Quaternion.Euler(rot)));//TODO: BIGBOMB
                break;
        }
    }

    Vector3 SetRandomTilePosition()
    {
        Vector3 vec;
        int totalTiles = tilesMap.Count;
        int randomIndex = UnityEngine.Random.Range(0, totalTiles - 1);
        Vector3 tilePos = tilesMap[randomIndex].transform.position;
        vec = new Vector3(tilePos.x, tilePos.y+1, tilePos.z);
        return vec;
    }

}
