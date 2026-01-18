public static class GameplaySettings
{
    public static InputType SelectedInputType { get; set; }
    public static Difficulty SelectedDifficulty { get; set; }
    public static GameMode SelectedMode { get; set; }
}

public enum InputType
{
    Mouse,
    Keyboard
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public enum GameMode
{
    PlayerVsNPC,
    PlayerVsPlayer,
}