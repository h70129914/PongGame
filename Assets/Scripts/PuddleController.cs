using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PuddleController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;
    [SerializeField] private PaddleController paddleController;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private PuddleMovement puddleMovement;
    private PaddleNPC paddleNPC;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        puddleMovement = new PuddleMovement();
        paddleNPC = new PaddleNPC();
    }

    void FixedUpdate()
    {
        int yMovement = 0;

        switch (paddleController)
        {
            case PaddleController.Player:
                yMovement = playerInput.YMovement;
                break;
            case PaddleController.NPC:
                yMovement = paddleNPC.CalculateMovement();
                break;
        }

        Vector3 newPosition = puddleMovement.CalculateNewPosition(transform.position, yMovement, speed, Time.fixedDeltaTime, yMin, yMax);
        rb.MovePosition(newPosition);
    }
}

public enum PaddleController
{
    Player,
    NPC
}