using UnityEngine;

/// <summary>
/// Manages the Pause System In Game
/// </summary>
public class PauseManager : MonoBehaviour
{
    // The pause menu to be used
    [SerializeField] private GameObject _pauseMenu;

    /// <summary>
    /// Pauses the game
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        _pauseMenu.SetActive(true);
    }

    /// <summary>
    /// Resumes the game
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        _pauseMenu.SetActive(false);
    }
}
