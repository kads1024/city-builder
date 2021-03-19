using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The Manager that controls transition between scenes
/// </summary>
public class SceneTransition : MonoBehaviour
{
    /// <summary>
    /// Transitions to the Game Over Screen
    /// </summary>
    public void OpenGameOverScreen()
    {
        SceneManager.LoadScene(Constants.GAME_OVER_SCENE);
    }

    /// <summary>
    /// Transitions to the main game scene
    /// </summary>
    public void OpenMainGameScene()
    {
        SceneManager.LoadScene(Constants.MAIN_GAME_SCENE);
    }

    /// <summary>
    /// Transitions to the main manu scene
    /// </summary>
    public void OpenMainMenuScene()
    {
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE);
    }

    /// <summary>
    /// Transitions to the Tutorial scene
    /// </summary>
    public void OpenTutorialScene()
    {
        SceneManager.LoadScene(Constants.TUTORIAL_SCENE);
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    public void CloseGame()
    {
        Application.Quit();
    }
}
