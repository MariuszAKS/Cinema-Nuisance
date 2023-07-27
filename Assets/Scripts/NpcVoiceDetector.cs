using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcVoiceDetector : MonoBehaviour
{
    [SerializeField] GameObject npc;
    [SerializeField] NpcController npcController;

    

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Voice")) {
            npcController.HeardSpoiler();
        }
    }
}
