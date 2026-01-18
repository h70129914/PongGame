using UnityEngine.UIElements;

public class MainMenuView
{
    // Window 1: Main Menu
    public VisualElement MainMenuContainer { get; private set; }
    public Button PlayButton { get; private set; }
    public Button LeaderboardButton { get; private set; }
    
    // Window 2: Control Selection
    public VisualElement ControlSelectionContainer { get; private set; }
    public Button MouseButton { get; private set; }
    public Button KeyboardButton { get; private set; }
    public Button ControlBackButton { get; private set; }
    
    // Window 3: Difficulty Selection
    public VisualElement DifficultySelectionContainer { get; private set; }
    public Button EasyButton { get; private set; }
    public Button MediumButton { get; private set; }
    public Button HardButton { get; private set; }
    public Button DifficultyBackButton { get; private set; }
    
    public VisualElement Root { get; private set; }

    public MainMenuView(VisualElement root)
    {
        Root = root;
        
        // Window 1: Main Menu
        MainMenuContainer = root.Q<VisualElement>("main-menu-container");
        PlayButton = root.Q<Button>("play-button");
        LeaderboardButton = root.Q<Button>("leaderboard-button");
        
        // Window 2: Control Selection
        ControlSelectionContainer = root.Q<VisualElement>("control-selection-container");
        MouseButton = root.Q<Button>("mouse-button");
        KeyboardButton = root.Q<Button>("keyboard-button");
        ControlBackButton = root.Q<Button>("control-back-button");
        
        // Window 3: Difficulty Selection
        DifficultySelectionContainer = root.Q<VisualElement>("difficulty-selection-container");
        EasyButton = root.Q<Button>("easy-button");
        MediumButton = root.Q<Button>("medium-button");
        HardButton = root.Q<Button>("hard-button");
        DifficultyBackButton = root.Q<Button>("difficulty-back-button");
    }

    public void ShowDifficultySelectionContainer(bool show)
    {
        if (DifficultySelectionContainer != null)
        {
            DifficultySelectionContainer.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }

    public void ShowControlSelectionContainer(bool show)
    {
        if (ControlSelectionContainer != null)
        {
            ControlSelectionContainer.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
