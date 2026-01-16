using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class ScoreManager
{
    private static int playerScore = 0;
    private const string LeaderboardFileName = "leaderboard.json";

    [System.Serializable]
    private class LeaderboardData
    {
        public List<int> scores = new();
    }

    public static int PlayerScore
    {
        get { return playerScore; }
        set { playerScore = value; }
    }

    public static void ResetScore()
    {
        playerScore = 0;
    }

    public static void UpdateScore()
    {
        string path = Path.Combine(Application.persistentDataPath, LeaderboardFileName);
        LeaderboardData leaderboard = new();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            if (!string.IsNullOrEmpty(json))
            {
                leaderboard = JsonUtility.FromJson<LeaderboardData>(json);
            }
        }

        // The list is kept sorted ascending.
        // Check if the new score qualifies for the top 10.
        if (leaderboard.scores.Count < 10 || playerScore > leaderboard.scores.First())
        {
            if (leaderboard.scores.Count >= 10)
            {
                // Remove the lowest score to make room for the new one.
                leaderboard.scores.RemoveAt(0);
            }
            
            leaderboard.scores.Add(playerScore);
            leaderboard.scores.Sort(); // Sorts ascending by default
        }

        string newJson = JsonUtility.ToJson(leaderboard);
        File.WriteAllText(path, newJson);
    }

    public static List<int> GetLeaderboard()
    {
        string path = Path.Combine(Application.persistentDataPath, LeaderboardFileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            if (!string.IsNullOrEmpty(json))
            {
                LeaderboardData leaderboard = JsonUtility.FromJson<LeaderboardData>(json);
                return leaderboard.scores;
            }
        }
        return new List<int>();
    }
}
