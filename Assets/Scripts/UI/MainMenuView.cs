using UnityEngine.UIElements;

public class MainMenuView
{
    public Button PlayButton { get; private set; }
    public Button LeaderboardButton { get; private set; }
    public VisualElement Root { get; private set; }

    public MainMenuView(VisualElement root)
    {
        Root = root;
        PlayButton = root.Q<Button>("play-button");
        LeaderboardButton = root.Q<Button>("leaderboard-button");
    }
}
