using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drown : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // TODO: Kill Player - FINAL
        }
    }
}
