using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private VisualTreeAsset entryTemplate;
    private LeaderboardView leaderboardView;

    void Start()
    {
        if (uiDocument == null)
        {
            uiDocument = GetComponent<UIDocument>();
        }

        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component not found on " + gameObject.name);
            return;
        }

        VisualElement root = uiDocument.rootVisualElement;
        leaderboardView = new LeaderboardView(root);

        if (entryTemplate == null)
        {
            Debug.LogError("LeaderboardEntry template not assigned in the inspector");
        }

        // Setup back button callback
        if (leaderboardView.BackButton != null)
        {
            leaderboardView.BackButton.clicked += OnBackButtonClicked;
        }

        // Hide leaderboard by default
        Hide();
    }

    private void OnBackButtonClicked()
    {
        Hide();
        
        MainMenuController mainMenuController = FindFirstObjectByType<MainMenuController>();
        if (mainMenuController != null)
        {
            mainMenuController.ShowMainMenu();
        }
    }

    public void Show()
    {
        if (leaderboardView != null && leaderboardView.Root != null)
        {
            leaderboardView.Root.style.display = DisplayStyle.Flex;
        }
    }

    public void Hide()
    {
        if (leaderboardView != null && leaderboardView.Root != null)
        {
            leaderboardView.Root.style.display = DisplayStyle.None;
        }
    }

    public void Populate()
    {
        if (leaderboardView == null || leaderboardView.EntriesContainer == null)
        {
            Debug.LogError("Leaderboard view not initialized");
            return;
        }

        if (entryTemplate == null)
        {
            Debug.LogError("Entry template not assigned in the inspector");
            return;
        }

        // Clear existing entries
        leaderboardView.EntriesContainer.Clear();

        // Get scores from ScoreManager
        List<int> scores = ScoreManager.GetLeaderboard();

        // Reverse to show highest scores first (since ScoreManager keeps them sorted ascending)
        for (int i = scores.Count - 1; i >= 0; i--)
        {
            int index = scores.Count - i;
            int score = scores[i];
            
            VisualElement entryElement = entryTemplate.CloneTree();
            LeaderboardEntryView entryView = new(entryElement);
            entryView.SetData(index, score);
            
            leaderboardView.EntriesContainer.Add(entryElement);
        }
    }
}
