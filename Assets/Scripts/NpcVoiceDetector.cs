using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcVoiceDetector : MonoBehaviour
{
    [SerializeField] GameObject npc;
    [SerializeField] NpcController npcController;

    private void Awake() {
        npcController = npc.GetComponent<NpcController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Voice")) {
            npcController.AnswerVoiceDebug();
        } else Debug.Log("Other collision?");
    }
}
