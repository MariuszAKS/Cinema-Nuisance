using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NpcController : MonoBehaviour
{
    [SerializeField] bool isAggressive;
    [SerializeField] int spoilersBeforeGettingUp;

    [SerializeField] AIDestinationSetter destination;

    [SerializeField] SpriteRenderer MoodSprRend;
    [SerializeField] Sprite MoodAnnoyed;
    [SerializeField] Sprite MoodGivenUp;
    [SerializeField] Sprite MoodAggressive;



    void Awake()
    {
        destination = GetComponent<AIDestinationSetter>();
        MoodSprRend.enabled = false;
    }

    public void HeardSpoiler() {
        spoilersBeforeGettingUp--;
        MoodSprRend.enabled = true;

        if (spoilersBeforeGettingUp <= 0) {
            if (isAggressive) {
                MoodSprRend.sprite = MoodAggressive;
                destination.target = GameObject.FindWithTag("Player").transform;
            } else {
                MoodSprRend.sprite = MoodGivenUp;
                destination.target = GameObject.FindWithTag("Exit").transform;
            }
        } else {
            MoodSprRend.sprite = MoodAnnoyed;
        }
    }
}