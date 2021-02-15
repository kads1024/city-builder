using UnityEngine;

/// <summary>
/// Responsible for tracking the task of the current unit
/// </summary>
public class TaskManager : MonoBehaviour
{
    // The current task of the unity
    private Coroutine _playerTask;

    /// <summary>
    /// Sets the current Task
    /// </summary>
    /// <param name="p_task">The current task to be set</param>
    public void SetTask(Coroutine p_task)
    {
        _playerTask = p_task;
    }

    /// <summary>
    /// Stops the current task
    /// </summary>
    public void StopCurrentTask()
    {
        StopCoroutine(_playerTask);
    }

    /// <summary>
    /// Resets the task value
    /// </summary>
    public void ResetTask()
    {
        _playerTask = null;
    }

    /// <summary>
    /// Check if there is a task for the player
    /// </summary>
    /// <returns></returns>
    public bool HasPendingTask()
    {
        return _playerTask != null; 
    }
}
