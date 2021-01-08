using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bomb : MonoBehaviourPunCallbacks
{
    [Header("Values")]
    public Vector3 color = Vector3.zero;
    [SerializeField]
    int tiles_to_paint = 2;

    [Header("GameObjects")]
    [SerializeField]
    GameObject bombBody;
    [SerializeField]
    GameObject Triggers;
    [SerializeField]
    GameObject BombPaint;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Destroy(bombBody);
        }

        Destroy(GetComponent<Animator>());

        CreatePaintTriggers();
    }

    void CreatePaintTriggers()
    {
        RaycastHit[] ray = new RaycastHit[4];
        Physics.Raycast(transform.position, new Vector3(1, 0, 0), out ray[0]);
        Physics.Raycast(transform.position, new Vector3(0, 0, 1), out ray[1]);
        Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out ray[2]);
        Physics.Raycast(transform.position, new Vector3(0, 0, -1), out ray[3]);

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

        Invoke("DestroyTriggers", 0.1f);
    }

    void DestroyTriggers()
    {
        Destroy(gameObject);
    }
}
