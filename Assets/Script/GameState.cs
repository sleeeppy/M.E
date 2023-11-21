using UnityEngine;

[System.Serializable]
public class GameState
{
    public float currentTime;
    public Vector3 playerPosition;

    public GameState()
    {

    }

    public GameState(float currentTime, Vector3 playerPosition)
    {
        this.currentTime = currentTime;
        this.playerPosition = playerPosition;
    }
}