using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NpcController : MonoBehaviour
{
    [SerializeField] bool isAggressive;
    [SerializeField] int spoilersBeforeGettingUp;

    [SerializeField] AIDestinationSetter destination;



    void Awake()
    {
        destination = GetComponent<AIDestinationSetter>();
    }

    public void HeardSpoiler() {
        spoilersBeforeGettingUp--;

        if (spoilersBeforeGettingUp <= 0) {
            if (isAggressive) {
                destination.target = GameObject.FindWithTag("Player").transform;
            } else {
                destination.target = GameObject.FindWithTag("Exit").transform;
            }
        }
    }
}