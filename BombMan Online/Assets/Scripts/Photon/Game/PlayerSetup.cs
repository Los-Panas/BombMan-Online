using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    private PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = transform.parent.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        PV.RPC("PRC_MOVE", RpcTarget.AllBuffered, transform.position);
    }

    [PunRPC]
    void RPC_MOVE(Vector3 t)
    {
        transform.position = t;
    }
}

