using UnityEngine;

/// <summary>
/// Acts as a bridge to animation events
/// </summary>
public class AnimationEventListener : MonoBehaviour
{
    // List of animation events to call when an animation event is called
    [SerializeField] private GameEvent _footStepEvent;
    [SerializeField] private GameEvent _attackEvent;

    public void FootstepEvent()
    {
        _footStepEvent.Raise();
    }

    public void AttackEvent()
    {
        _attackEvent.Raise();
    }

    public void SetAttackEvent(GameEvent p_event)
    {
        _attackEvent = p_event;
    }
}
