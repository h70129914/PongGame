using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TextMeshProUGUI HighestScoreText;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private Button closeButton;


    private void Start()
    {
        ScoreManager.ScoreChanged += UpdateScore;
        gameOverMenu.SetActive(false);
    }

    private void OnDestroy()
    {
        ScoreManager.ScoreChanged -= UpdateScore;

    }
    public void UpdateScore(int score) => playerScoreText.text = score.ToString();

    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        ScoreText.text = $"Score: {ScoreManager.PlayerScore}";
        HighestScoreText.text = $"Highest Score: {ScoreManager.GetHighestScore()}";

        // Make sure to add a CanvasGroup to the gameOverMenu GameObject
        if (!gameOverMenu.TryGetComponent<CanvasGroup>(out var canvasGroup))
        {
            canvasGroup = gameOverMenu.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);

        closeButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }
}
