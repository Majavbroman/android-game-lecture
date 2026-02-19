using UnityEngine;
public class HealthHeart
{
    private HeartState _state;
    private HeartType _type;

    public HealthHeart(HeartType type, HeartState state)
    {
        _type = type;
        _state = state;
    }

    public void SetState(HeartState newState)
    {
        _state = newState;
        // Update the heart's visual representation here
    }
}
