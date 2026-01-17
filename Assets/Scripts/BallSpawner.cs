using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner instance;
    [SerializeField] private BallController ballPrefab;
    private bool direction;

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
        BallController newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        // set random value move direction based on direction parameter
        newBall.MoveDirection = new Vector2(Random.Range(0.5f, 1), 1) * (direction ? 1 : -1);
        direction = !direction;
    }
}
