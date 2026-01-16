using UnityEngine;

public class PuddleMovement
{
    public Vector3 CalculateNewPosition(Vector3 currentPosition, int yMovement, float speed, float deltaTime, float yMin, float yMax)
    {
        if (yMovement != 0)
        {
            Vector3 newPosition = currentPosition + Vector3.up * yMovement * speed * deltaTime;
            newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);
            return newPosition;
        }

        return currentPosition;
    }
}
