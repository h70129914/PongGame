using UnityEngine;

public static class BallPrediction
{
    public static float PredictYAtX(
        Vector2 ballPos,
        Vector2 ballVelocity,
        float targetX,
        float topWallY,
        float bottomWallY
    )
    {
        // If ball is moving away from NPC, just return current Y
        if (Mathf.Approximately(ballVelocity.x, 0f))
            return ballPos.y;

        float timeToReachX = (targetX - ballPos.x) / ballVelocity.x;

        if (timeToReachX < 0f)
            return ballPos.y;

        float rawY = ballPos.y + ballVelocity.y * timeToReachX;

        float height = topWallY - bottomWallY;
        float doubleHeight = height * 2f;

        float modY = (rawY - bottomWallY) % doubleHeight;
        if (modY < 0) modY += doubleHeight;

        float reflectedY = bottomWallY + (modY <= height
            ? modY
            : doubleHeight - modY);

        return reflectedY;
    }
}
