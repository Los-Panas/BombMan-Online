using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBombTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.layer = 2;
        }
    }
}
