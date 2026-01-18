using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPaddle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    private Camera mainCamera;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        MoveWithPlayerInput();
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