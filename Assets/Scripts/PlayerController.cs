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