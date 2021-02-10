using UnityEngine;

/// <summary>
/// Player Input Helps in modifying input for the player
/// </summary>
public class PlayerInput : MonoBehaviour
{
    private enum MouseButton {
        LeftMouseButton,
        RightMouseButton
    }

    // Options for selecting the mouse input
    [SerializeField] private MouseButton _selectionButton;
    [SerializeField] private MouseButton _interactionButton;

    #region CLICK
    public bool OnSelectionClick()
    {
        return Input.GetMouseButtonDown((int)_selectionButton);
    }

    public bool OnInteractionClick()
    {
        return Input.GetMouseButtonDown((int)_interactionButton);
    }
    #endregion

    #region HOLD
    public bool OnSelectionHold()
    {
        return Input.GetMouseButton((int)_selectionButton);
    }

    public bool OnInteractionHold()
    {
        return Input.GetMouseButton((int)_interactionButton);
    }
    #endregion

    #region UP
    public bool OnSelectionUp()
    {
        return Input.GetMouseButtonUp((int)_selectionButton);
    }

    public bool OnInteractionUp()
    {
        return Input.GetMouseButtonUp((int)_interactionButton);
    }
    #endregion
}

