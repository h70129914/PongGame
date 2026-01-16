using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ScoreManagerTests
{
    private string leaderboardPath;

    [SetUp]
    public void Setup()
    {
        // Path for the test leaderboard file
        leaderboardPath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        
        // Ensure the file doesn't exist before each test
        if (File.Exists(leaderboardPath))
        {
            File.Delete(leaderboardPath);
        }
        
        // Reset the score in the manager
        ScoreManager.ResetScore();
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up the file after each test
        if (File.Exists(leaderboardPath))
        {
            File.Delete(leaderboardPath);
        }
    }

    [Test]
    public void ResetScore_SetsPlayerScoreToZero()
    {
        ScoreManager.PlayerScore = 100;
        ScoreManager.ResetScore();
        Assert.AreEqual(0, ScoreManager.PlayerScore);
    }

    [Test]
    public void GetLeaderboard_ReturnsEmptyListWhenNoFileExists()
    {
        var leaderboard = ScoreManager.GetLeaderboard();
        Assert.IsNotNull(leaderboard);
        Assert.AreEqual(0, leaderboard.Count);
    }

    [Test]
    public void UpdateScore_AddsScoreToEmptyLeaderboard()
    {
        ScoreManager.PlayerScore = 50;
        ScoreManager.UpdateScore();
        
        var leaderboard = ScoreManager.GetLeaderboard();
        
        Assert.AreEqual(1, leaderboard.Count);
        Assert.Contains(50, leaderboard);
    }

    [Test]
    public void UpdateScore_AddsMultipleScoresAndMaintainsAscendingOrder()
    {
        ScoreManager.PlayerScore = 50;
        ScoreManager.UpdateScore();
        ScoreManager.PlayerScore = 20;
        ScoreManager.UpdateScore();
        ScoreManager.PlayerScore = 80;
        ScoreManager.UpdateScore();

        var leaderboard = ScoreManager.GetLeaderboard();

        Assert.AreEqual(3, leaderboard.Count);
        Assert.AreEqual(20, leaderboard[0]);
        Assert.AreEqual(50, leaderboard[1]);
        Assert.AreEqual(80, leaderboard[2]);
    }
    
    [Test]
    public void UpdateScore_DoesNotAddScoreLowerThanTopTen()
    {
        // 1. Arrange: Create a full leaderboard with scores 10 through 100
        for (int i = 1; i <= 10; i++)
        {
            ScoreManager.PlayerScore = i * 10;
            ScoreManager.UpdateScore();
        }

        // 2. Act: Try to add a score that is too low
        ScoreManager.PlayerScore = 5;
        ScoreManager.UpdateScore();
        
        // 3. Assert: The low score should not be in the leaderboard
        var leaderboard = ScoreManager.GetLeaderboard();
        Assert.AreEqual(10, leaderboard.Count);
        Assert.IsFalse(leaderboard.Contains(5));
        Assert.AreEqual(10, leaderboard.First()); // The lowest score should still be 10
    }

    [Test]
    public void UpdateScore_ReplacesLowestScoreWhenNewScoreIsHigher()
    {
        // 1. Arrange: Create a full leaderboard with scores 10 through 100
        for (int i = 1; i <= 10; i++)
        {
            ScoreManager.PlayerScore = i * 10;
            ScoreManager.UpdateScore();
        }
        
        // 2. Act: Try to add a score that should replace the lowest
        ScoreManager.PlayerScore = 15;
        ScoreManager.UpdateScore();
        
        // 3. Assert: 15 should be in, 10 should be out
        var leaderboard = ScoreManager.GetLeaderboard();
        Assert.AreEqual(10, leaderboard.Count);
        Assert.IsTrue(leaderboard.Contains(15));
        Assert.IsFalse(leaderboard.Contains(10));
        Assert.AreEqual(15, leaderboard.First());
    }

    [Test]
    public void UpdateScore_DoesNotExceedTenScores()
    {
        // Arrange: Add 12 scores
        for (int i = 1; i <= 12; i++)
        {
            ScoreManager.PlayerScore = i * 10;
            ScoreManager.UpdateScore();
        }

        // Act
        var leaderboard = ScoreManager.GetLeaderboard();
        var expectedTop10 = new List<int> { 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };

        // Assert
        Assert.AreEqual(10, leaderboard.Count);
        CollectionAssert.AreEqual(expectedTop10, leaderboard);
    }
}
