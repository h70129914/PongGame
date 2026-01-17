using TMPro;
using UnityEngine;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScoreText;

    private void Start()
    {
        ScoreManager.ScoreChanged += UpdateScore;
    }

    private void OnDestroy()
    {
        ScoreManager.ScoreChanged -= UpdateScore;

    }
    public void UpdateScore(int score) => playerScoreText.text = score.ToString();
}
