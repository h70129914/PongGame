using UnityEngine.UIElements;

public class LeaderboardView
{
    public Button BackButton { get; private set; }
    public Label TitleLabel { get; private set; }
    public ScrollView EntriesContainer { get; private set; }
    public VisualElement Root { get; private set; }

    public LeaderboardView(VisualElement root)
    {
        Root = root;
        BackButton = root.Q<Button>("back-button");
        TitleLabel = root.Q<Label>("title-label");
        EntriesContainer = root.Q<ScrollView>("entries-container");
    }
}
