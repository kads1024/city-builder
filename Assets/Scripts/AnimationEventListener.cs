using UnityEngine;

/// <summary>
/// Acts as a bridge to animation events
/// </summary>
public class AnimationEventListener : MonoBehaviour
{
    // List of animation events to call when an animation event is called
    [SerializeField] private GameEvent _footStepEvent;
    [SerializeField] public GameEvent _attackEvent;

    public void FootstepEvent()
    {
        _footStepEvent.Raise();
    }

    public void AttackEvent()
    {
        _attackEvent.Raise();
    }
}
