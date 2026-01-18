using UnityEngine;

public class PaddleSpawner : MonoBehaviour
{
    [SerializeField] private Transform topWall;
    [SerializeField] private Transform bottomWall;
    [SerializeField] private Transform leftPaddleSpawnPoint;
    [SerializeField] private Transform rightPaddleSpawnPoint;
    [SerializeField] private PlayerPaddle playerPaddlePrefab;
    [SerializeField] private NPCPaddle npcPaddlePrefab;

    private void Start()
    {
        SpawnPaddles();
    }

    private void SpawnPaddles()
    {
        if (GameplaySettings.SelectedMode == GameMode.PlayerVsNPC)
        {
            Instantiate(playerPaddlePrefab, leftPaddleSpawnPoint.position, Quaternion.identity);
            Instantiate(npcPaddlePrefab, rightPaddleSpawnPoint.position, Quaternion.identity);
        }
        else if (GameplaySettings.SelectedMode == GameMode.PlayerVsPlayer)
        {
            Instantiate(playerPaddlePrefab, leftPaddleSpawnPoint.position, Quaternion.identity);
            Instantiate(npcPaddlePrefab, rightPaddleSpawnPoint.position, Quaternion.identity);
        }
    }
}
