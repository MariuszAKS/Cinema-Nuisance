using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.transform.parent.tag);

        if (other.transform.parent.CompareTag("Npc")) {
            GameController.instance.NpcExitsCinema(other.transform.parent.gameObject);
        }
        else if (other.transform.parent.CompareTag("Player")) {
            Debug.Log("Player exits");
        }
    }
}
