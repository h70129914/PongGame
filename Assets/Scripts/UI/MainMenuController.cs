using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private MainMenuView mainMenuView;
    private LeaderboardController leaderboardController;

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
        mainMenuView = new MainMenuView(root);

        // Find LeaderboardController in the scene
        leaderboardController = FindFirstObjectByType<LeaderboardController>();
        if (leaderboardController == null)
        {
            Debug.LogWarning("LeaderboardController not found in the scene");
        }

        // Setup button callbacks
        if (mainMenuView.PlayButton != null)
        {
            mainMenuView.PlayButton.clicked += OnPlayButtonClicked;
        }

        if (mainMenuView.LeaderboardButton != null)
        {
            mainMenuView.LeaderboardButton.clicked += OnLeaderboardButtonClicked;
        }
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("GamePlay");
    }

    private void OnLeaderboardButtonClicked()
    {
        if (mainMenuView != null && mainMenuView.Root != null)
        {
            mainMenuView.Root.style.display = DisplayStyle.None;
        }
        
        if (leaderboardController != null)
        {
            leaderboardController.Show();
            leaderboardController.Populate();
        }
    }

    public void ShowMainMenu()
    {
        if (mainMenuView != null && mainMenuView.Root != null)
        {
            mainMenuView.Root.style.display = DisplayStyle.Flex;
        }
    }
}
