using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float sensitivity;
    public int YMovement { get; set; }
    private Camera mainCamera;

    private void Start()
    {
        Cursor.visible = false;
        mainCamera = Camera.main;
    }
    private void OnDestroy()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        switch (GameplaySettings.SelectedInputType)
        {
            case InputType.Mouse:
                ReadMouseInput();
                break;
            case InputType.Keyboard:
                ReadKeyboardInput();
                break;
        }
    }

    private void ReadKeyboardInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            YMovement = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            YMovement = -1;
        }
        else
        {
            YMovement = 0;
        }
    }

    private void ReadMouseInput()
    {
        // Mouse screen position
        Vector3 mouseScreenPos = Input.mousePosition;

        // Convert to world position
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        float mouseY = Input.GetAxis("Mouse Y");
        if (mouseY > sensitivity)
        {
            YMovement = 1;
        }
        else if (mouseY < -sensitivity)
        {
            YMovement = -1;
        }
        else
        {
            YMovement = 0;
        }
    }
}

public enum InputType
{
    Keyboard,
    Mouse,
}
