using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Acts as a bridge to animation events
/// </summary>
public class AnimationEventListener : MonoBehaviour
{
    // List of animation events to call when an animation event is called
    [SerializeField] private GameEvent _footStepEvent;
    [SerializeField] private UnityEvent _attackEvent;

    public void FootstepEvent()
    {
        _footStepEvent.Raise();
    }

    public void AttackEvent()
    {
        _attackEvent.Invoke();
    }

    public void SetAttackEvent(UnityAction p_event)
    {
        _attackEvent.RemoveAllListeners();
        _attackEvent.AddListener(p_event);
    }
}
