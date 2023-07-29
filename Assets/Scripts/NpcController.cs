using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NpcController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;

    [SerializeField] bool isAggressive;
    [SerializeField] int spoilersBeforeGettingUp;

    AIDestinationSetter destination;
    AIPath aiPath;
    bool followingPlayer;

    [SerializeField] SpriteRenderer MoodSprRend;
    [SerializeField] Sprite MoodAnnoyed;
    [SerializeField] Sprite MoodGivenUp;
    [SerializeField] Sprite MoodAggressive;



    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        destination = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        MoodSprRend.enabled = false;
    }

    void FixedUpdate()
    {
        animator.SetFloat("velocityX", aiPath.desiredVelocity.x);
        animator.SetFloat("velocityY", aiPath.desiredVelocity.y);

        if (followingPlayer && aiPath.reachedDestination) {
            GameController.instance.NpcCatchesPlayer();
        }
    }



    public void HeardSpoiler() {
        spoilersBeforeGettingUp--;
        MoodSprRend.enabled = true;

        if (spoilersBeforeGettingUp <= 0) {
            if (isAggressive) {
                MoodSprRend.sprite = MoodAggressive;
                destination.target = GameObject.FindWithTag("Player").transform;
                followingPlayer = true;
            } else {
                MoodSprRend.sprite = MoodGivenUp;
                destination.target = GameObject.FindWithTag("Exit").transform;
                followingPlayer = false;
            }
        } else {
            MoodSprRend.sprite = MoodAnnoyed;
        }
    }

    public void SetSpoilersAmount(ushort value) {
        spoilersBeforeGettingUp = value;
    }

    public void SetAggressive(bool value) {
        isAggressive = value;
    }
}