using NUnit.Framework;
using UnityEngine;

public class NPCPaddleSpeedTests
{
    private GameObject paddleGO;
    private NPCPaddle paddle;

    [SetUp]
    public void Setup()
    {
        paddleGO = new GameObject("NPCPaddle");
        paddle = paddleGO.AddComponent<NPCPaddle>();
        paddle.moveSpeed = 10f;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(paddleGO);
    }

    [TestCase(Difficulty.Hard, 10f)]
    [TestCase(Difficulty.Medium, 7.5f)]
    [TestCase(Difficulty.Easy, 5f)]
    public void CalculateMovementSpeed_ReturnsExpectedSpeed(Difficulty difficulty, float expectedSpeed)
    {
        GameplaySettings.SelectedDifficulty = difficulty;

        float speed = paddle.CalculateMovementSpeed();

        Assert.AreEqual(expectedSpeed, speed);
    }
}
