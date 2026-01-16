using UnityEngine;

public class PaddleNPC
{
    private BallController ball;
    private const float movementThreshold = 0.01f;

    public BallController Ball
    {
        get
        {
            if (!ball)
                ball = Object.FindAnyObjectByType<BallController>();
            return ball;
        }
        set
        {
            ball = value;
        }
    }

    /// <summary>
    /// Calculates the Y movement direction for the NPC paddle based on the ball's movement direction.
    /// Returns 1 for upward movement, -1 for downward movement, or 0 for no movement.
    /// </summary>
    /// <returns>Y movement direction: 1 (up), -1 (down), or 0 (no movement)</returns>
    public int CalculateMovement()
    {
        if (Ball == null)
        {
            return 0;
        }

        Vector2 ballMoveDirection = Ball.MoveDirection;
        
        if (ballMoveDirection.y > movementThreshold)
        {
            return 1; // Move up
        }
        else if (ballMoveDirection.y < -movementThreshold)
        {
            return -1; // Move down
        }
        else
        {
            return 0; // No movement
        }
    }
}
