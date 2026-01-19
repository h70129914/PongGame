using NUnit.Framework;
using System.Reflection;
using UnityEngine;

public class BallControllerTests
{
    private BallController ball;

    [SetUp]
    public void Setup()
    {
        GameObject go = new();
        ball = go.AddComponent<BallController>();
    }

    // ----------------------------------------
    // IncreaseSpeed Tests
    // ----------------------------------------

    [Test]
    public void IncreaseSpeed_MultipliesSpeedBy1Point25()
    {
        SetPrivateField("currentSpeed", 4f);
        SetPrivateField("startingSpeed", 2f);
        SetPrivateField("maxSpeed", 10f);

        float result = ball.IncreaseSpeed();

        Assert.AreEqual(5f, result);
    }

    [Test]
    public void IncreaseSpeed_DoesNotExceedMaxSpeed()
    {
        SetPrivateField("currentSpeed", 9f);
        SetPrivateField("startingSpeed", 2f);
        SetPrivateField("maxSpeed", 10f);

        float result = ball.IncreaseSpeed();

        Assert.AreEqual(10f, result);
    }

    [Test]
    public void IncreaseSpeed_DoesNotGoBelowStartingSpeed()
    {
        SetPrivateField("currentSpeed", 1f);
        SetPrivateField("startingSpeed", 2f);
        SetPrivateField("maxSpeed", 10f);

        float result = ball.IncreaseSpeed();

        Assert.AreEqual(2f, result);
    }

    // ----------------------------------------
    // Bounce Tests (direct comparison)
    // ----------------------------------------

    [Test]
    public void Bounce_ReflectsDirectionAgainstNormal()
    {
        ball.MoveDirection = Vector2.right;
        Vector3 ballPos = Vector3.zero;
        Vector3 colliderPos = Vector3.right;
        Vector2 collisionPoint = new(1, 0);

        ball.Bounce(ballPos, colliderPos, collisionPoint);

        // Direct comparison
        Assert.AreEqual(Vector2.left, ball.MoveDirection);
    }

    [Test]
    public void Bounce_UsesColliderPositionIfNormalIsZero()
    {
        ball.MoveDirection = Vector2.up;
        Vector3 ballPos = Vector3.zero;
        Vector3 colliderPos = Vector3.down;
        Vector2 collisionPoint = Vector2.zero; // normal will be zero

        ball.Bounce(ballPos, colliderPos, collisionPoint);

        Assert.AreEqual(Vector2.down, ball.MoveDirection);
    }


    [Test]
    public void SetInitialDirection_FirstCall_HasNegativeY()
    {
        ResetStaticDirection();

        ball.SetInitialDirection();

        Assert.Less(ball.MoveDirection.y, 0f);
    }

    [Test]
    public void SetInitialDirection_SecondCall_HasPositiveY()
    {
        ResetStaticDirection();

        ball.SetInitialDirection(); // 1st
        ball.SetInitialDirection(); // 2nd

        Assert.Greater(ball.MoveDirection.y, 0f);
    }

    [Test]
    public void SetInitialDirection_ThirdCall_HasNegativeYAgain()
    {
        ResetStaticDirection();

        ball.SetInitialDirection(); // 1st
        ball.SetInitialDirection(); // 2nd
        ball.SetInitialDirection(); // 3rd

        Assert.Less(ball.MoveDirection.y, 0f);
    }


    // ----------------------------------------
    // Helper for private fields
    // ----------------------------------------

    private void SetPrivateField(string fieldName, float value)
    {
        typeof(BallController)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(ball, value);
    }

    private void ResetStaticDirection()
    {
        typeof(BallController)
            .GetField("direction", BindingFlags.NonPublic | BindingFlags.Static)
            .SetValue(ball, false);
    }
}
