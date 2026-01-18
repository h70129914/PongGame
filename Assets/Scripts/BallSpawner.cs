using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner instance;
    [SerializeField] private BallController ballPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }
}
