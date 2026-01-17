using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameplayUIController gameplayUIController;

    void Start()
    {
        gameplayUIController = FindAnyObjectByType<GameplayUIController>();
        GoalConroller.GoalHit += OnGoalHit;
    }

    private void OnDestroy()
    {
        GoalConroller.GoalHit -= OnGoalHit;
    }

    private void OnGoalHit(GoalOwner goalOwner)
    {
        switch (goalOwner)
        {
            case GoalOwner.Player:
                OnPlayerGoalHit();
                break;
            case GoalOwner.NPC:
                OnNPCGoalHit();
                break;
            default:
                break;
        }
    }

    private void OnPlayerGoalHit()
    {
        ScoreManager.UpdateScore();
        gameplayUIController.ShowGameOverMenu();
    }

    private void OnNPCGoalHit()
    {
        ScoreManager.PlayerScore++;
        BallSpawner.instance.Spawn();
    }
}
