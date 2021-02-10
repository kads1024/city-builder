using UnityEngine;

/// <summary>
/// Utility is a helper class to help getting common methods without the need to copy paste code
/// </summary>
public static class Utility
{
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
}
