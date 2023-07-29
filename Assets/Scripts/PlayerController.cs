using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Camera camera;

    private Rigidbody2D rb2d;
    private PlayerInputActions playerInputActions;

    [SerializeField] private float speedWalk;
    [SerializeField] private float speedRun;
    
    [SerializeField] private GameObject VoiceRange;

    [SerializeField] private Animator animator;



    void Awake()
    {
        camera = Camera.main;

        rb2d = GetComponent<Rigidbody2D>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Speech.started += Speak;
        playerInputActions.Player.Speech.canceled += Speak;

        VoiceRange.SetActive(false);
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleCamera();

        animator.SetFloat("velocityX", rb2d.velocity.x);
        animator.SetFloat("velocityY", rb2d.velocity.y);
    }

    void OnDestroy() {
        playerInputActions.Player.Speech.started -= Speak;
        playerInputActions.Player.Speech.canceled -= Speak;
    }



    void HandleMovement() {
        Vector2 moveVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        float moveSpeed = playerInputActions.Player.Run.ReadValue<float>() == 0 ? speedWalk : speedRun;

        rb2d.velocity = moveVector * moveSpeed;
    }

    private void HandleCamera() {
        camera.transform.position = new Vector3(0, transform.position.y, -10);
    }

    public void Speak(InputAction.CallbackContext context) {
        VoiceRange.SetActive(!context.canceled);
    }
}
