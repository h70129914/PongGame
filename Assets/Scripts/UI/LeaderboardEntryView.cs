using UnityEngine.UIElements;

public class LeaderboardEntryView
{
    public Label IndexLabel { get; private set; }
    public Label ScoreLabel { get; private set; }
    public VisualElement Root { get; private set; }

    public LeaderboardEntryView(VisualElement root)
    {
        Root = root;
        IndexLabel = root.Q<Label>("index-label");
        ScoreLabel = root.Q<Label>("score-label");
    }

    public void SetData(int index, int score)
    {
        if (IndexLabel != null)
        {
            IndexLabel.text = index.ToString() + ".";
        }

        if (ScoreLabel != null)
        {
            ScoreLabel.text = score.ToString();
        }
    }
}
