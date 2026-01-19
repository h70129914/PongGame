using TMPro;
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
        float movement = MoveWithPlayerInput();
        float clampedY = Mathf.Clamp(movement, yMin, yMax);
        Vector2 targetPosition = new(rb.position.x, clampedY);
        rb.MovePosition(targetPosition);
    }

    private float MoveWithPlayerInput()
    {
        return GameplaySettings.SelectedInputType switch
        {
            InputType.Mouse => MoveWithMouse(),
            InputType.Keyboard => MoveWithKeyboard(),
            _ => 0,
        };
    }

    private float MoveWithMouse()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        return mouseWorldPos.y;
    }

    private float MoveWithKeyboard()
    {
        float inputY = Input.GetAxisRaw("Vertical");
        float newY = rb.position.y + inputY * speed * Time.fixedDeltaTime;
        return newY;
    }
}