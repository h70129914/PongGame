using System;
using UnityEngine;

public class GoalConroller : MonoBehaviour
{
    public GoalOwner goalOwner;
    public static event Action<GoalOwner> GoalHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            GoalHit?.Invoke(goalOwner);
        }
    }
}

public enum GoalOwner
{
    Player, 
    NPC
}
