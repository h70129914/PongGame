using NUnit.Framework;
using UnityEngine;

public class PuddleMovementTests
{
    private PuddleMovement _puddleMovement;

    [SetUp]
    public void Setup()
    {
        _puddleMovement = new PuddleMovement();
    }

    [Test]
    public void NoMovement_When_YMovementIsZero()
    {
        // Arrange
        var currentPosition = Vector3.zero;
        var newPosition = _puddleMovement.CalculateNewPosition(currentPosition, 0, 1, 1, -1, 1);
        Assert.AreEqual(currentPosition, newPosition);
    }

    [Test]
    public void NoMovement_When_SpeedIsZero()
    {
        var currentPosition = Vector3.zero;
        var newPosition = _puddleMovement.CalculateNewPosition(currentPosition, 1, 0, 1, -1, 1);
        Assert.AreEqual(currentPosition, newPosition);
    }

    [Test]
    public void MoveUpward_When_YMovementIsOne()
    {
        var currentPosition = Vector3.zero;
        var newPosition = _puddleMovement.CalculateNewPosition(currentPosition, 1, 1, 1, -1, 1);
        Assert.AreEqual(new Vector3(0,1,0), newPosition);
    }

    [Test]
    public void MoveDownward_When_YMovementIsMinusOne()
    {
        var currentPosition = Vector3.zero;
        var newPosition = _puddleMovement.CalculateNewPosition(currentPosition, -1, 1, 1, -1, 1);
        Assert.AreEqual(new Vector3(0,-1,0), newPosition);
    }

    [Test]
    public void MovementRestricted_At_YMax()
    {
        var currentPosition = new Vector3(0, 0.9f, 0);
        var newPosition = _puddleMovement.CalculateNewPosition(currentPosition, 1, 1, 1, -1, 1);
        Assert.AreEqual(new Vector3(0, 1, 0), newPosition);
    }

    [Test]
    public void MovementRestricted_At_YMin()
    {
        var currentPosition = new Vector3(0, -0.9f, 0);
        var newPosition = _puddleMovement.CalculateNewPosition(currentPosition, -1, 1, 1, -1, 1);
        Assert.AreEqual(new Vector3(0, -1, 0), newPosition);
    }
}
