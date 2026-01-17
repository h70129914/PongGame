using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class ScoreManager
{
    private static int playerScore = 0;
    private static List<int> scores = new();
    private const string LeaderboardFileName = "leaderboard.json";
    public static event Action<int> ScoreChanged;
    private static bool isSavingDisabled = false;

    [Serializable]
    private class LeaderboardData
    {
        public List<int> scores = new();
    }

    public static int PlayerScore
    {
        get { return playerScore; }
        set
        {
            playerScore = value;
            ScoreChanged?.Invoke(playerScore);
        }
    }

    public static void ResetScore()
    {
        PlayerScore = 0;
    }

    public static void UpdateScore()
    {
        if (playerScore <= 0)
            return;

        AddScoreToList(playerScore);

        if (!isSavingDisabled)
            Save();
    }

    private static void AddScoreToList(int newScore)
    {
        if (scores.Count < 10 || newScore > scores.First())
        {
            if (scores.Count >= 10)
            {
                scores.RemoveAt(0);
            }

            scores.Add(newScore);
            scores.Sort();
        }
    }

    public static void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, LeaderboardFileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            if (!string.IsNullOrEmpty(json))
            {
                LeaderboardData leaderboard = JsonUtility.FromJson<LeaderboardData>(json);
                scores = leaderboard.scores ?? new List<int>();
            }
        }
        else
        {
            scores = new List<int>();
        }
    }

    public static void Save()
    {
        string path = Path.Combine(Application.persistentDataPath, LeaderboardFileName);
        LeaderboardData leaderboard = new() { scores = scores };
        string newJson = JsonUtility.ToJson(leaderboard);
        File.WriteAllText(path, newJson);
    }

    public static List<int> GetLeaderboard()
    {
        return new List<int>(scores);
    }

    public static int GetHighestScore()
    {
        if (scores.Count > 0)
        {
            return scores.Max();
        }
        return 0;
    }
    
    public static void ResetLeaderboard()
    {
        scores.Clear();
    }

    public static void DisableSavingForTests(bool disableSaving)
    {
        isSavingDisabled = disableSaving;
    }
}
