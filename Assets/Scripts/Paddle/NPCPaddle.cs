using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NPCPaddle : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;

    public float reactionDistance = 3f; 

    [Header("Arena Bounds")]
    public float topWallY;
    public float bottomWallY;

    private Rigidbody2D paddleRigidbody;
    private Rigidbody2D ballRigidbody;

    void Start()
    {
        paddleRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!ballRigidbody)
        {
            var ball = GameObject.FindGameObjectWithTag("Ball");
            if (ball)
                ballRigidbody = ball.GetComponent<Rigidbody2D>();
            else
                return;
        }

        bool isBallMovingTowardNPC = ballRigidbody.linearVelocity.x < 0f; // Assuming NPC is on the left side
        if (!isBallMovingTowardNPC)
            return;

        float targetY = CalculateMovementPosition(paddleRigidbody.position, ballRigidbody.position, ballRigidbody.linearVelocity);
        float currentMoveSpeed = CalculateMovementSpeed();
        MoveToTarget(targetY, currentMoveSpeed);
    }

    public float CalculateMovementPosition(Vector2 paddlePosition, Vector2 ballPosition, Vector2 ballVelocity)
    {
        Difficulty difficulty = GameplaySettings.SelectedDifficulty;
        float targetY = paddlePosition.y;
        
        switch (difficulty)
        {
            case Difficulty.Hard:
                targetY = BallPrediction.PredictYAtX(
                    ballPosition,
                    ballVelocity,
                    paddlePosition.x,
                    topWallY,
                    bottomWallY
                );
                break;

            case Difficulty.Medium:
                targetY = ballPosition.y;
                break;

            case Difficulty.Easy:
                float distanceToBall = Mathf.Abs(ballPosition.x - paddlePosition.x);
                if (distanceToBall < reactionDistance)
                    targetY = ballPosition.y;
                break;
        }

        return targetY;
    }

    public float CalculateMovementSpeed()
    {
        Difficulty difficulty = GameplaySettings.SelectedDifficulty;
        return difficulty switch
        {
            Difficulty.Hard => moveSpeed,
            Difficulty.Medium => moveSpeed * 0.75f,
            Difficulty.Easy => moveSpeed * 0.5f,
            _ => moveSpeed,
        };
    }

    private void MoveToTarget(float targetY, float currentMoveSpeed)
    {
        float newY = Mathf.MoveTowards(
            paddleRigidbody.position.y,
            targetY,
            currentMoveSpeed * Time.fixedDeltaTime
        );

        newY = Mathf.Clamp(newY, bottomWallY, topWallY);

        paddleRigidbody.MovePosition(new Vector2(paddleRigidbody.position.x, newY));
    }
}
