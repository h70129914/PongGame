using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PuddleController : MonoBehaviour
{
    public float speed;
    public float yMin;
    public float yMax;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private PuddleMovement puddleMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        puddleMovement = new PuddleMovement();
    }

    void FixedUpdate()
    {
        Vector3 newPosition = puddleMovement.CalculateNewPosition(transform.position, playerInput.YMovement, speed, Time.fixedDeltaTime, yMin, yMax);
        rb.MovePosition(newPosition);
    }
}