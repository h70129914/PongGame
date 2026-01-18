using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public InputType CurrentInputType;
    public int YMovement { get; set; } 

    void Update()
    {
        if (CurrentInputType == InputType.Mouse)
        {
            float mouseY = Input.GetAxis("Mouse Y");
            if (mouseY > 0.1f)
            {
                YMovement = 1;
            }
            else if (mouseY < -0.1f)
            {
                YMovement = -1;
            }
            else
            {
                YMovement = 0;
            }
        }
        else
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
    }
}

public enum InputType
{
    Keyboard,
    Mouse,
}
