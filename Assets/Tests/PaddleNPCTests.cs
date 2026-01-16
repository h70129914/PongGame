using NUnit.Framework;
using UnityEngine;

public class PaddleNPCTests
{
    private PaddleNPC paddleNPC;
    private GameObject ballGameObject;
    private BallController ballController;

    [SetUp]
    public void Setup()
    {
        paddleNPC = new PaddleNPC();
        
        // Create a test ball GameObject with BallController
        ballGameObject = new GameObject("TestBall");
        ballController = ballGameObject.AddComponent<BallController>();
        
        // Set the ball in PaddleNPC
        paddleNPC.Ball = ballController;
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up test objects
        if (ballGameObject != null)
        {
            Object.DestroyImmediate(ballGameObject);
        }
    }

    [Test]
    public void CalculateMovement_ReturnsOne_WhenBallMovesUpward()
    {
        // Arrange
        ballController.MoveDirection = new Vector2(0.5f, 1f); // Moving upward

        // Act
        int result = paddleNPC.CalculateMovement();

        // Assert
        Assert.AreEqual(1, result, "Should return 1 when ball moves upward");
    }

    [Test]
    public void CalculateMovement_ReturnsNegativeOne_WhenBallMovesDownward()
    {
        // Arrange
        ballController.MoveDirection = new Vector2(0.5f, -1f); // Moving downward

        // Act
        int result = paddleNPC.CalculateMovement();

        // Assert
        Assert.AreEqual(-1, result, "Should return -1 when ball moves downward");
    }

    [Test]
    public void CalculateMovement_ReturnsZero_WhenBallMovesHorizontally()
    {
        // Arrange
        ballController.MoveDirection = new Vector2(1f, 0f); // Moving horizontally

        // Act
        int result = paddleNPC.CalculateMovement();

        // Assert
        Assert.AreEqual(0, result, "Should return 0 when ball moves horizontally");
    }

    [Test]
    public void CalculateMovement_ReturnsZero_WhenBallMoveDirectionIsZero()
    {
        // Arrange
        ballController.MoveDirection = Vector2.zero;

        // Act
        int result = paddleNPC.CalculateMovement();

        // Assert
        Assert.AreEqual(0, result, "Should return 0 when ball direction is zero");
    }

    [Test]
    public void CalculateMovement_ReturnsOne_WhenBallMovesUpwardWithSmallYComponent()
    {
        // Arrange
        ballController.MoveDirection = new Vector2(0.9f, 0.1f); // Small upward component above threshold

        // Act
        int result = paddleNPC.CalculateMovement();

        // Assert
        Assert.AreEqual(1, result, "Should return 1 when ball has small upward movement above threshold");
    }

    [Test]
    public void CalculateMovement_ReturnsNegativeOne_WhenBallMovesDownwardWithSmallYComponent()
    {
        // Arrange
        ballController.MoveDirection = new Vector2(0.9f, -0.1f); // Small downward component below threshold

        // Act
        int result = paddleNPC.CalculateMovement();

        // Assert
        Assert.AreEqual(-1, result, "Should return -1 when ball has small downward movement below threshold");
    }

    [Test]
    public void CalculateMovement_ReturnsZero_WhenBallYComponentIsBelowThreshold()
    {
        // Arrange
        ballController.MoveDirection = new Vector2(0.9f, 0.005f); // Y component below 0.01 threshold

        // Act
        int result = paddleNPC.CalculateMovement();

        // Assert
        Assert.AreEqual(0, result, "Should return 0 when ball Y component is below threshold");
    }

    [Test]
    public void CalculateMovement_ReturnsZero_WhenBallIsNull()
    {
        // Arrange
        paddleNPC.Ball = null;

        // Act
        int result = paddleNPC.CalculateMovement();

        // Assert
        Assert.AreEqual(0, result, "Should return 0 when ball is null");
    }

    [Test]
    public void CalculateMovement_HandlesDiagonalMovementUpward()
    {
        // Arrange
        ballController.MoveDirection = new Vector2(0.707f, 0.707f).normalized; // 45-degree upward diagonal

        // Act
        int result = paddleNPC.CalculateMovement();

        // Assert
        Assert.AreEqual(1, result, "Should return 1 for diagonal upward movement");
    }

    [Test]
    public void CalculateMovement_HandlesDiagonalMovementDownward()
    {
        // Arrange
        ballController.MoveDirection = new Vector2(0.707f, -0.707f).normalized; // 45-degree downward diagonal

        // Act
        int result = paddleNPC.CalculateMovement();

        // Assert
        Assert.AreEqual(-1, result, "Should return -1 for diagonal downward movement");
    }

    [Test]
    public void BallProperty_CanBeSetAndRetrieved()
    {
        // Arrange
        GameObject newBallGameObject = new GameObject("NewTestBall");
        BallController newBallController = newBallGameObject.AddComponent<BallController>();

        // Act
        paddleNPC.Ball = newBallController;
        BallController retrievedBall = paddleNPC.Ball;

        // Assert
        Assert.AreEqual(newBallController, retrievedBall, "Ball property should return the set ball");

        // Cleanup
        Object.DestroyImmediate(newBallGameObject);
    }

    [Test]
    public void CalculateMovement_WorksAfterBallIsReassigned()
    {
        // Arrange
        ballController.MoveDirection = new Vector2(0f, 1f);
        int initialResult = paddleNPC.CalculateMovement();
        
        // Create new ball with different direction
        GameObject newBallGameObject = new GameObject("NewTestBall");
        BallController newBallController = newBallGameObject.AddComponent<BallController>();
        newBallController.MoveDirection = new Vector2(0f, -1f);

        // Act
        paddleNPC.Ball = newBallController;
        int newResult = paddleNPC.CalculateMovement();

        // Assert
        Assert.AreEqual(1, initialResult, "Initial ball should move upward");
        Assert.AreEqual(-1, newResult, "New ball should move downward");

        // Cleanup
        Object.DestroyImmediate(newBallGameObject);
    }
}
