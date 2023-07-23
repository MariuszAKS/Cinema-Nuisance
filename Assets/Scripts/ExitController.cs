using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Npc")) {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player")) {
            Debug.Log("Player exits");
        }
    }
}
