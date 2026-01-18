using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NPCPaddle : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;

    [Header("Arena Bounds")]
    public float topWallY;
    public float bottomWallY;

    private Rigidbody2D rb;
    private Rigidbody2D ballRb;
    private float targetY;
    private float currentMoveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        if (ballRb == null)
        {
            var ball = GameObject.FindGameObjectWithTag("Ball");
            if (ball)
                ballRb = ball.GetComponent<Rigidbody2D>();
            else
                return;
        }

        Difficulty difficulty = GameplaySettings.SelectedDifficulty;

        switch (difficulty)
        {
            case Difficulty.Hard:
                // Use ball prediction for hard difficulty
                currentMoveSpeed = moveSpeed; // Full speed for hard
                if (ballRb.linearVelocity.x < 0f) // Only predict if ball is moving toward NPC
                {
                    targetY = BallPrediction.PredictYAtX(
                        ballRb.position,
                        ballRb.linearVelocity,
                        rb.position.x,
                        topWallY,
                        bottomWallY
                    );
                }
                break;

            case Difficulty.Medium:
                // Medium: Follow ball's current Y position with slight delay
                // Only react when ball is moving toward NPC
                currentMoveSpeed = moveSpeed * 0.75f; // 75% speed for medium
                if (ballRb.linearVelocity.x < 0f)
                {
                    targetY = ballRb.position.y;
                }
                break;

            case Difficulty.Easy:
                // Easy: Follow ball's current Y position slowly, only when ball is close
                currentMoveSpeed = moveSpeed * 0.5f; // 50% speed for easy
                float distanceToBall = Mathf.Abs(ballRb.position.x - rb.position.x);
                float reactionDistance = 3f; // Only react when ball is within this distance
                
                if (ballRb.linearVelocity.x < 0f && distanceToBall < reactionDistance)
                {
                    targetY = ballRb.position.y;
                }
                break;
        }

        MoveToTarget();
    }

    void MoveToTarget()
    {
        float newY = Mathf.MoveTowards(
            rb.position.y,
            targetY,
            currentMoveSpeed * Time.fixedDeltaTime
        );

        newY = Mathf.Clamp(newY, bottomWallY, topWallY);

        rb.MovePosition(new Vector2(rb.position.x, newY));
    }
}
