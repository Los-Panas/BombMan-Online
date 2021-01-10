using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonDestroyAvatar : MonoBehaviour
{
    public static PhotonDestroyAvatar DA;
    // Start is called before the first frame update
    void Start()
    {
        if(DA == null)
        {
            DA = this;
        }
    }

    // Update is called once per frame
    public void DestroyAvatar()
    {
         PhotonNetwork.Destroy(gameObject);
    }
}
