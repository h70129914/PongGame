using NUnit.Framework;
using UnityEngine;

public class NPCPaddlePositionTests
{
    private NPCPaddle paddle;

    [SetUp]
    public void Setup()
    {
        paddle = new GameObject("NPCPaddle").AddComponent<NPCPaddle>();

        paddle.reactionDistance = 3f;
        paddle.topWallY = 5f;
        paddle.bottomWallY = -5f;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(paddle.gameObject);
    }

    // ------------------------
    // MEDIUM
    // ------------------------

    [Test]
    public void CalculateMovementPosition_Medium_FollowsBallY()
    {
        GameplaySettings.SelectedDifficulty = Difficulty.Medium;

        Vector2 paddlePos = new(0f, 1f);
        Vector2 ballPos = new(5f, 4f);
        Vector2 velocity = Vector2.zero;

        float targetY = paddle.CalculateMovementPosition(
            paddlePos,
            ballPos,
            velocity
        );

        Assert.AreEqual(4f, targetY);
    }

    // ------------------------
    // EASY
    // ------------------------

    [Test]
    public void CalculateMovementPosition_Easy_BallInsideReactionDistance_FollowsBall()
    {
        GameplaySettings.SelectedDifficulty = Difficulty.Easy;

        Vector2 paddlePos = new(0f, 1f);
        Vector2 ballPos = new(2f, 3f); // distance = 2 < 3
        Vector2 velocity = Vector2.zero;

        float targetY = paddle.CalculateMovementPosition(
            paddlePos,
            ballPos,
            velocity
        );

        Assert.AreEqual(3f, targetY);
    }

    [Test]
    public void CalculateMovementPosition_Easy_BallOutsideReactionDistance_StaysInPlace()
    {
        GameplaySettings.SelectedDifficulty = Difficulty.Easy;

        Vector2 paddlePos = new(0f, 1f);
        Vector2 ballPos = new(10f, 5f); // distance = 10 > 3
        Vector2 velocity = Vector2.zero;

        float targetY = paddle.CalculateMovementPosition(
            paddlePos,
            ballPos,
            velocity
        );

        Assert.AreEqual(1f, targetY);
    }
}
