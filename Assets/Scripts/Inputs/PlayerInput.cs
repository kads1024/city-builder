using UnityEngine;

/// <summary>
/// Player Input Helps in modifying input for the player
/// </summary>
[CreateAssetMenu(menuName = "Inputs/Player Input")]
public class PlayerInput : ScriptableObject
{
    private enum MouseButton
    {
        LeftMouseButton,
        RightMouseButton
    }

    private const string MOUSE_SCROLL_WHEEL = "Mouse ScrollWheel";

    // Options for selecting the mouse input
    [Header("Mouse Buttons")]
    [SerializeField] private MouseButton _selectionButton;
    [SerializeField] private MouseButton _interactionButton;

    // Options for selecting the mouse input
    [Header("Keyboard Buttons")]
    [SerializeField] private KeyCode _cameraUp;
    [SerializeField] private KeyCode _cameraDown;
    [SerializeField] private KeyCode _cameraLeft;
    [SerializeField] private KeyCode _cameraRight;

    /// <summary>
    /// Camera Movement Controls
    /// </summary>
    /// <returns>The normalized direction where the camera will move</returns>
    public Vector3 CameraDirection()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(_cameraUp)) direction += Vector3.forward;
        if (Input.GetKey(_cameraDown)) direction += Vector3.back;
        if (Input.GetKey(_cameraLeft)) direction += Vector3.left;
        if (Input.GetKey(_cameraRight)) direction += Vector3.right;

        return direction.normalized;
    }

    // Mouse Buttons
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

    #region SCROLL
    public float MouseScroll()
    {
        return Input.GetAxis(MOUSE_SCROLL_WHEEL);
    }
    #endregion
}

