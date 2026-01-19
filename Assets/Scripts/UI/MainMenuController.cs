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

        // Setup Window 1: Main Menu button callbacks
        SetupWindow1();

        // Setup Window 1 (Game Mode Selection): Game Mode Selection button callbacks
        SetupWindow1GameMode();

        // Setup Window 2: Control Selection button callbacks
        SetupWindow2();

        // Setup Window 3: Difficulty Selection button callbacks
        SetupWindow3();

        // First window: Select Game Mode
        ShowGameModeSelection();
        HideControlSelection();
        HideDifficultySelection();
    }

    private void SetupWindow1()
    {
        if (mainMenuView.PlayButton != null)
        {
            mainMenuView.PlayButton.clicked += OnPlayButtonClicked;
        }

        if (mainMenuView.LeaderboardButton != null)
        {
            mainMenuView.LeaderboardButton.clicked += OnLeaderboardButtonClicked;
        }
    }

    private void SetupWindow1GameMode()
    {
        if (mainMenuView.OnePlayerButton != null)
        {
            mainMenuView.OnePlayerButton.clicked += OnOnePlayerButtonClicked;
        }

        if (mainMenuView.GameModeBackButton != null)
        {
            mainMenuView.GameModeBackButton.clicked += OnGameModeBackButtonClicked;
        }

        // Two Players button is disabled - no functionality yet
    }

    private void SetupWindow2()
    {
        if (mainMenuView.MouseButton != null)
        {
            mainMenuView.MouseButton.clicked += OnMouseButtonClicked;
        }

        if (mainMenuView.KeyboardButton != null)
        {
            mainMenuView.KeyboardButton.clicked += OnKeyboardButtonClicked;
        }

        if (mainMenuView.ControlBackButton != null)
        {
            mainMenuView.ControlBackButton.clicked += OnControlBackButtonClicked;
        }
    }

    private void SetupWindow3()
    {
        if (mainMenuView.EasyButton != null)
        {
            mainMenuView.EasyButton.clicked += OnEasyButtonClicked;
        }

        if (mainMenuView.MediumButton != null)
        {
            mainMenuView.MediumButton.clicked += OnMediumButtonClicked;
        }

        if (mainMenuView.HardButton != null)
        {
            mainMenuView.HardButton.clicked += OnHardButtonClicked;
        }

        if (mainMenuView.DifficultyBackButton != null)
        {
            mainMenuView.DifficultyBackButton.clicked += OnDifficultyBackButtonClicked;
        }
    }

    private void OnPlayButtonClicked()
    {
        ShowGameModeSelection();
    }

    private void OnOnePlayerButtonClicked()
    {
        GameplaySettings.SelectedMode = GameMode.PlayerVsNPC;
        ShowControlSelection();
    }

    private void OnGameModeBackButtonClicked()
    {
        ShowMainMenu();
    }

    private void OnLeaderboardButtonClicked()
    {
        HideMainMenu();

        if (leaderboardController != null)
        {
            leaderboardController.Show();
            leaderboardController.Populate();
        }
    }

    private void OnMouseButtonClicked() => SelectInputType(InputType.Mouse);

    private void OnKeyboardButtonClicked() => SelectInputType(InputType.Keyboard);

    private void SelectInputType(InputType inputType)
    {
        GameplaySettings.SelectedInputType = inputType;
        ShowDifficultySelection();
    }

    private void OnControlBackButtonClicked() => ShowGameModeSelection();

    private void OnEasyButtonClicked() => SelectDeficulty(Difficulty.Easy);

    private void OnMediumButtonClicked() => SelectDeficulty(Difficulty.Medium);

    private void OnHardButtonClicked() => SelectDeficulty(Difficulty.Hard);

    private void SelectDeficulty(Difficulty difficulty)
    {
        GameplaySettings.SelectedDifficulty = difficulty;
        StartGameplay();
    }

    private void OnDifficultyBackButtonClicked()
    {
        ShowControlSelection();
    }

    private void StartGameplay()
    {
        Debug.Log($"Starting gameplay with Input Type: {GameplaySettings.SelectedInputType}, Difficulty: {GameplaySettings.SelectedDifficulty}");
        SceneManager.LoadScene("Gameplay");
    }

    public void ShowMainMenu()
    {
        mainMenuView.ShowMainMenuContainer(true);
        HideGameModeSelection();
        HideControlSelection();
        HideDifficultySelection();
    }

    private void HideMainMenu() => mainMenuView.ShowMainMenuContainer(false);

    private void ShowGameModeSelection()
    {
        mainMenuView.ShowGameModeSelectionContainer(true);
        HideMainMenu();
        HideControlSelection();
        HideDifficultySelection();
    }

    private void HideGameModeSelection() => mainMenuView.ShowGameModeSelectionContainer(false);

    private void ShowControlSelection()
    {
        mainMenuView.ShowControlSelectionContainer(true);
        HideMainMenu();
        HideGameModeSelection();
        HideDifficultySelection();
    }

    private void HideControlSelection() => mainMenuView.ShowControlSelectionContainer(false);

    private void ShowDifficultySelection()
    {
        mainMenuView.ShowDifficultySelectionContainer(true);
        HideMainMenu();
        HideGameModeSelection();
        HideControlSelection();
    }

    private void HideDifficultySelection() => mainMenuView.ShowDifficultySelectionContainer(false);
}


