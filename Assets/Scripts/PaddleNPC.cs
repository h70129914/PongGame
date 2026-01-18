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
