using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NpcController : MonoBehaviour
{
    [SerializeField] bool isAggressive;

    [SerializeField] AIDestinationSetter destination;



    void Awake()
    {
        destination = GetComponent<AIDestinationSetter>();
    }

    void FixedUpdate()
    {
        
    }

    public void SetPlayerAsTarget()
    {
        destination.target = GameObject.FindWithTag("Player").transform;
    }

    public void AnswerVoiceDebug()
    {
        Debug.Log(name + " heard that!");
    }
}