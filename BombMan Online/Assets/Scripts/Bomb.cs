﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bomb : MonoBehaviourPunCallbacks
{
    [Header("Values")]
    public Color color;
    public int tiles_to_paint = 2;
    [SerializeField]
    LayerMask ignoreMask;

    [Header("GameObjects")]
    [SerializeField]
    GameObject bombBody;
    [SerializeField]
    GameObject Triggers;
    [SerializeField]
    GameObject BombPaint;
    [SerializeField]
    ParticleSystemRenderer PaintExplosion;

    [HideInInspector]
    public CharacterSkinController.RobotColor bomb_color = CharacterSkinController.RobotColor.None;

    public void Explode()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Destroy(bombBody);
        }

        Destroy(GetComponent<Animator>());

        PaintExplosion.gameObject.SetActive(true);
        PaintExplosion.material.color = color;
        Invoke("Destroy", 1.5f);

        CreatePaintTriggers();

        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayAudioWithName("explosion");
    }

    void CreatePaintTriggers()
    {
        RaycastHit[] ray = new RaycastHit[4];
        Physics.Raycast(transform.position, new Vector3(1, 0, 0), out ray[0], 100, ignoreMask.value);
        Physics.Raycast(transform.position, new Vector3(0, 0, 1), out ray[1], 100, ignoreMask.value);
        Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out ray[2], 100, ignoreMask.value);
        Physics.Raycast(transform.position, new Vector3(0, 0, -1), out ray[3], 100, ignoreMask.value);

        Instantiate(BombPaint, transform.position, BombPaint.transform.rotation, Triggers.transform);

        for (int i = 0; i < 4; ++i)
        {
            Vector3 direction = Vector3.zero;
            switch (i)
            {
                case 0:
                    direction = new Vector3(1, 0, 0);
                    break;
                case 1:
                    direction = new Vector3(0, 0, 1);
                    break;
                case 2:
                    direction = new Vector3(-1, 0, 0);
                    break;
                case 3:
                    direction = new Vector3(0, 0, -1);
                    break;
            }

            int j = 1;
            while (ray[i].distance > j && j <= tiles_to_paint)
            {
                Instantiate(BombPaint, transform.position + direction * j, BombPaint.transform.rotation, Triggers.transform);
                ++j;
            }
        }

        Destroy(GetComponent<Collider>());
        Invoke("DestroyTriggers", 0.05f);
    }

    void DestroyTriggers()
    {
        Destroy(Triggers);
        TileManager.instance.UpdateValues();
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
