using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Utility is a helper class to help getting common methods without the need to copy paste code
/// </summary>
public static class Utility
{
    /// <summary>
    /// Check if mouse is over a ui element
    /// </summary>
    /// <returns>Whether or not the mouse is over a UI element</returns>
    public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    /// <summary>
    /// Finds where the mouse hits the Terrain
    /// </summary>
    /// <returns>The point in the ground where the mouse position is pointing at the Terrain</returns>
    public static Vector3 MouseToTerrainPosition()
    {
        Vector3 position = Vector3.zero;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info, 100, LayerMask.GetMask("Ground")))
            position = info.point;
        return position;
    }

    /// <summary>
    /// Returns the Object where the mouse is pointing at
    /// </summary>
    /// <returns>The Object where the mouse is pointing at</returns>
    public static RaycastHit MouseToObject()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info, 100))
            return info;
        return new RaycastHit();
    }

    /// <summary>
    /// Used as a selection box for selecting units
    /// </summary>
    private static Texture2D _whiteTexture;
    public static Texture2D WhiteTexture
    {
        get
        {
            if (!_whiteTexture) {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }
            return _whiteTexture;
        }
    }

    /// <summary>
    /// Used to visualize the WhiteTexture
    /// </summary>
    /// <param name="p_rect">The rectangle to be drawn</param>
    /// <param name="p_color">The Color of the rectangle</param>
    public static void DrawScreenRect(Rect p_rect, Color p_color)
    {
        GUI.color = p_color;
        GUI.DrawTexture(p_rect, WhiteTexture);
        GUI.color = Color.white;
    }

    /// <summary>
    /// Used to visualize the WhiteTexture borders
    /// </summary>
    /// <param name="p_rect">The rectangle border to be drawn</param>
    /// <param name="p_thickness">Height of the rectangle border</param>
    /// <param name="p_color">The Color of the rectangle border</param>
    public static void DrawScreenRectBorder(Rect p_rect, float p_thickness, Color p_color)
    {
        // Top
        DrawScreenRect(new Rect(p_rect.xMin, p_rect.yMin, p_rect.width, p_thickness), p_color);
        // Left
        DrawScreenRect(new Rect(p_rect.xMin, p_rect.yMin, p_thickness, p_rect.height), p_color);
        // Right
        DrawScreenRect(new Rect(p_rect.xMax - p_thickness, p_rect.yMin, p_thickness, p_rect.height), p_color);
        // Bottom
        DrawScreenRect(new Rect(p_rect.xMin, p_rect.yMax - p_thickness, p_rect.width, p_thickness), p_color);
    }

    /// <summary>
    /// The Rectangle to be drawn based on the two positions
    /// </summary>
    /// <param name="p_screenPosition1">First point of the rectangle</param>
    /// <param name="p_screenPosition2">Second Point of the rectangle</param>
    /// <returns>The rectangle that was drawn based on the two positions</returns>
    public static Rect GetScreenRect(Vector3 p_screenPosition1, Vector3 p_screenPosition2)
    {
        // Move origin from bottom left to top left
        p_screenPosition1.y = Screen.height - p_screenPosition1.y;
        p_screenPosition2.y = Screen.height - p_screenPosition2.y;

        // Calculate corners
        var topLeft = Vector3.Min(p_screenPosition1, p_screenPosition2);
        var bottomRight = Vector3.Max(p_screenPosition1, p_screenPosition2);

        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

}
