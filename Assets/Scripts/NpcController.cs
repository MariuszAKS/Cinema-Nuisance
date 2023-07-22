using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] bool isAggressive;



    void Awake()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("UpsettingRange")) {
            Debug.Log(name + "Upset");
        } else {
            Debug.Log("Other trigger");
        }
    }
}