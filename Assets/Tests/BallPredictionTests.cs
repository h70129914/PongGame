using NUnit.Framework;
using UnityEngine;

public class BallPredictionTests
{
    private const float TopWall = 5f;
    private const float BottomWall = -5f;

    [Test]
    public void PredictYAtX_BallNotMovingInX_ReturnsCurrentY()
    {
        Vector2 ballPos = new(0f, 2f);
        Vector2 velocity = new(0f, 3f);

        float y = BallPrediction.PredictYAtX(
            ballPos,
            velocity,
            targetX: -10f,
            TopWall,
            BottomWall
        );

        Assert.AreEqual(2f, y);
    }

    [Test]
    public void PredictYAtX_BallMovingAway_ReturnsCurrentY()
    {
        Vector2 ballPos = new(0f, 1f);
        Vector2 velocity = new(5f, 2f); // moving right
        float targetX = -10f; // NPC on left

        float y = BallPrediction.PredictYAtX(
            ballPos,
            velocity,
            targetX,
            TopWall,
            BottomWall
        );

        Assert.AreEqual(1f, y);
    }

    [Test]
    public void PredictYAtX_NoWallHit_ReturnsLinearPrediction()
    {
        Vector2 ballPos = new(0f, 0f);
        Vector2 velocity = new(-5f, 1f);
        float targetX = -10f;

        float y = BallPrediction.PredictYAtX(
            ballPos,
            velocity,
            targetX,
            TopWall,
            BottomWall
        );

        Assert.AreEqual(2f, y, 0.001f);
    }

    [Test]
    public void PredictYAtX_SingleBounce_ReflectsCorrectly()
    {
        Vector2 ballPos = new(0f, 4f);
        Vector2 velocity = new(-5f, 2f);
        float targetX = -10f;

        float y = BallPrediction.PredictYAtX(
            ballPos,
            velocity,
            targetX,
            TopWall,
            BottomWall
        );

        Assert.AreEqual(2f, y, 0.001f);
    }

    [Test]
    public void PredictYAtX_MultipleBounces_StillCorrect()
    {
        Vector2 ballPos = new(0f, 0f);
        Vector2 velocity = new(-1f, 7f);
        float targetX = -10f;

        float y = BallPrediction.PredictYAtX(
            ballPos,
            velocity,
            targetX,
            TopWall,
            BottomWall
        );

        Assert.That(y, Is.InRange(BottomWall, TopWall));
    }
}
