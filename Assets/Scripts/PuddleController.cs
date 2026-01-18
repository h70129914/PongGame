using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PuddleController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;
    [SerializeField] private PaddleController paddleController;

    private Camera mainCamera;
    private Rigidbody2D rb;

    private PaddleNPC paddleNPC;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        paddleNPC = new PaddleNPC();
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        switch (paddleController)
        {
            case PaddleController.Player:
                MoveWithPlayerInput();
                break;
            case PaddleController.NPC:
                MoveWithNPC();
                break;
        }
    }

    private void MoveWithNPC()
    {
        int inputY = paddleNPC.CalculateMovement();

        float newY = rb.position.y + inputY * speed * Time.fixedDeltaTime;

        newY = Mathf.Clamp(newY, yMin, yMax);

        rb.MovePosition(new Vector2(rb.position.x, newY));
    }

    private void MoveWithPlayerInput()
    {
        switch (GameplaySettings.SelectedInputType)
        {
            case InputType.Mouse:
                MoveWithMouse();
                break;
            case InputType.Keyboard:
                MoveWithKeyboard();
                break;
        }
    }

    private void MoveWithMouse()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        float clampedY = Mathf.Clamp(mouseWorldPos.y, yMin, yMax);

        Vector2 targetPosition = new(
            rb.position.x,
            clampedY
        );

        rb.MovePosition(targetPosition);
    }

    private void MoveWithKeyboard()
    {
        float inputY = Input.GetAxisRaw("Vertical");

        float newY = rb.position.y + inputY * speed * Time.fixedDeltaTime;

        newY = Mathf.Clamp(newY, yMin, yMax);

        rb.MovePosition(new Vector2(rb.position.x, newY));
    }
}

public enum PaddleController
{
    Player,
    NPC
}