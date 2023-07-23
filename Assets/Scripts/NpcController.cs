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

    public void AnswerVoiceDebug()
    {
        Debug.Log(name + " heard that!");
    }
}