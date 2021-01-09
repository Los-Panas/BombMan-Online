﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
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
    int puType;
    PhotonView pv;
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
        if (Time.time - spawnTimer > spawnTime && spawnedPowerUps.Count < maxPowerUps)
        {
            spawnTime = UnityEngine.Random.Range(5f, 10f);
            spawnTimer = Time.time;
            puType = UnityEngine.Random.Range(1, 3);
            Debug.Log("TYPE: " + puType);
            Vector3 pos = SetRandomTilePosition();
            pv.RPC("InstantiatePowerUp", RpcTarget.AllBufferedViaServer, new object[] { puType, pos });
            //InstantiatePowerUp(puType);
        }
    }
    [PunRPC]
    private void InstantiatePowerUp(int puType, Vector3 pos)
    {
        switch ((PUTypes)puType)
        {
            case PUTypes.SPEED:
                spawnedPowerUps.Add(Instantiate(speedPU, pos, Quaternion.identity));//TODO: BIGBOMB
                Debug.Log("SPEED");
                break;
            case PUTypes.BIG_BOMB:
                spawnedPowerUps.Add(Instantiate(bombPU, pos, Quaternion.identity));//TODO: BIGBOMB
                Debug.Log("BOMB");
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