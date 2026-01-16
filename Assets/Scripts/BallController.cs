using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb2D;
    private Vector2 moveDirection;

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

        moveDirection = new Vector2(0.5f, 0.75f);
    }

    void FixedUpdate()
    {
        if (rb2D != null && moveDirection != Vector2.zero)
        {
            rb2D.linearVelocity = moveDirection * speed;
        }
    }

    public void Bounce(Vector2 normal)
    {
        // Reflect the move direction off the normal
        moveDirection = Vector2.Reflect(moveDirection, normal).normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paddle") || other.CompareTag("Wall"))
        {
            // Calculate the normal based on the collision point
            Vector2 collisionPoint = other.ClosestPoint(transform.position);
            Vector2 normal = ((Vector2)transform.position - collisionPoint).normalized;

            // If normal is zero (edge case), use the direction from other to this
            if (normal == Vector2.zero)
            {
                normal = (transform.position - other.transform.position).normalized;
            }

            Bounce(normal);
        }
        if(other.CompareTag("Goal"))
        {
            // Reset ball position and direction
            transform.position = Vector2.zero;
            moveDirection = new Vector2(0.5f, 0.75f).normalized;
        }
    }
}
