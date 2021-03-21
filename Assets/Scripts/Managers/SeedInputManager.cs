using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SeedInputManager : MonoBehaviour
{
    public GameObject SeedMenu;
    public StringReference Seed;
    public TMP_InputField SeedInput;
    public SceneTransition SceneManager;

    public void OnClickPlay()
    {
        SeedMenu.SetActive(true);
    }

    public void PlayGame()
    {
        Seed.SetVariableValue(SeedInput.text);
        SceneManager.OpenMainGameScene();   
    }
}
