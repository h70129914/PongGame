using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float startingSpeed = 5f;
    [SerializeField] private float maxSpeed = 10;
    private float currentSpeed;
    private Rigidbody2D rb2D;
    private Vector2 moveDirection;
    private static bool direction;

    public Vector2 MoveDirection
    {
        get { return moveDirection; }
        set { moveDirection = value.normalized; }
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
        {
            Debug.LogError("Rigidbody2D component not found on " + gameObject.name);
        }
        currentSpeed = startingSpeed;
        SetInitialDirection();
    }

    private void FixedUpdate()
    {
        if (rb2D != null && moveDirection != Vector2.zero)
        {
            rb2D.linearVelocity = moveDirection * currentSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paddle"))
        {
            currentSpeed = Mathf.Clamp(currentSpeed *= 1.25f, startingSpeed, maxSpeed);
            
            Bounce(other);
        }

        else if (other.CompareTag("Wall"))
            Bounce(other);
    }

    private void SetInitialDirection()
    {
        MoveDirection = new Vector2(Random.Range(0.5f, 1), 1) * (direction ? 1 : -1);
        direction = !direction;
    }


    private void Bounce(Collider2D other)
    {
        // Calculate the normal based on the collision point
        Vector2 collisionPoint = other.ClosestPoint(transform.position);
        Vector2 normal = ((Vector2)transform.position - collisionPoint).normalized;

        // If normal is zero (edge case), use the direction from other to this
        if (normal == Vector2.zero)
        {
            normal = (transform.position - other.transform.position).normalized;
        }
        // Reflect the move direction off the normal
        moveDirection = Vector2.Reflect(moveDirection, normal).normalized;
        
    }

}
