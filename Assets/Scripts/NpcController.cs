using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NpcController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;

    [SerializeField] private bool isAggressive;
    [SerializeField] private int spoilersBeforeGettingUp;

    private AIDestinationSetter destination;
    private AIPath aiPath;
    private GameObject voiceSource;
    private bool followingPlayer;

    [SerializeField] private SpriteRenderer MoodSprRend;
    [SerializeField] private Sprite MoodAnnoyed;
    [SerializeField] private Sprite MoodGivenUp;
    [SerializeField] private Sprite MoodAggressive;

    private GameObject Player;
    private Rigidbody2D PlayerRb2d;



    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        destination = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        MoodSprRend.enabled = false;
    }

    void Start() {
        Player = GameObject.FindWithTag("Player");
        PlayerRb2d = Player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        animator.SetFloat("velocityX", aiPath.desiredVelocity.x);
        animator.SetFloat("velocityY", aiPath.desiredVelocity.y);

        if (isAggressive && destination.target != null) {
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

            if (distanceToPlayer < 2 && PlayerRb2d.velocity != Vector2.zero) {
                FollowPlayer();
            } else if (distanceToPlayer > 6) {
                FollowDoor();
            }
        }

        if (aiPath.reachedDestination) {
            if (followingPlayer) {
                GameController.instance.NpcCatchesPlayer();
            } else {
                Destroy(voiceSource);

                float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
                if (distanceToPlayer < 2) {
                    FollowPlayer();
                } else {
                    FollowDoor();
                }
            }
        }
    }



    public void SetSpoilersAmount(ushort value) {
        spoilersBeforeGettingUp = value;
    }
    public void SetAggressive(bool value) {
        isAggressive = value;
    }

    public void HeardSpoiler() {
        spoilersBeforeGettingUp--;
        MoodSprRend.enabled = true;

        if (spoilersBeforeGettingUp <= 0) {
            if (isAggressive) {
                MoodSprRend.sprite = MoodAggressive;
                FollowVoice();
            } else {
                MoodSprRend.sprite = MoodGivenUp;
                FollowDoor();
            }
        } else {
            MoodSprRend.sprite = MoodAnnoyed;
        }
    }

    private void FollowVoice() {
        voiceSource = new GameObject("Voice Source");
        voiceSource.transform.position = Player.transform.position;
        destination.target = voiceSource.transform;
        followingPlayer = false;
    }
    private void FollowPlayer() {
        destination.target = Player.transform;
        followingPlayer = true;
    }
    private void FollowDoor() {
        GameObject[] Exits = GameObject.FindGameObjectsWithTag("Exit");
        destination.target = Exits[Random.Range(0, Exits.Length)].transform;
        followingPlayer = false;
    }
}