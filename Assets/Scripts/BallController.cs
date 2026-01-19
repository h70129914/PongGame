using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    [SerializeField] private float startingSpeed = 5f;
    [SerializeField] private float maxSpeed = 10;
    private float currentSpeed;
    private Rigidbody2D rb2D;

    private static bool direction;

    public Vector2 MoveDirection { get; set; }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currentSpeed = startingSpeed;
        SetInitialDirection();
    }

    private void FixedUpdate()
    {
        rb2D.linearVelocity = MoveDirection * currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 collisionPoint = other.ClosestPoint(transform.position);
        Vector3 otherPosition = other.transform.position;

        bool shouldBounce = false;

        if (other.CompareTag("Paddle"))
        {
            currentSpeed = IncreaseSpeed();
            shouldBounce = true;
        }
        else if (other.CompareTag("Wall"))
        {
            shouldBounce = true;
        }

        if (shouldBounce)
            Bounce(transform.position, otherPosition, collisionPoint);
    }

    public void SetInitialDirection()
    {
        var randomDirection = new Vector2(Random.Range(0.5f, 1), 1).normalized;
        MoveDirection = randomDirection * (direction ? 1 : -1);
        direction = !direction;
    }

    public float IncreaseSpeed() => Mathf.Clamp(currentSpeed *= 1.25f, startingSpeed, maxSpeed);

    public void Bounce(Vector3 ballPosition, Vector3 colliderPosition, Vector2 collisionPoint)
    {
        Vector2 normal = ((Vector2)ballPosition - collisionPoint).normalized;

        if (normal == Vector2.zero)
            normal = (ballPosition - colliderPosition).normalized;

        MoveDirection = Vector2.Reflect(MoveDirection, normal).normalized;
    }
}
