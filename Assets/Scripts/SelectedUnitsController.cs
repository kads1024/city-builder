using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resonsible for controlling all selected units in the game
/// </summary>
public class SelectedUnitsController : MonoBehaviour
{
    // Input that will be controlling the Units
    [SerializeField] private PlayerInput _input;

    // The list of ground objects that the units will walk on
    [SerializeField] private GameObjectRuntimeSet _groundSet;

    // List of all currently selected units
    [SerializeField] private SelectableContainer _selectedUnits;

    private void Update()
    {
        if(_input.OnInteractionUp())
        {
            Collider hitObject = Utility.MouseToObject().collider;
            
            // Check if mouse click hits the ground
            if(_groundSet.Items.Contains(hitObject.gameObject))
            {
                // Iterate through each unit
                foreach(GameObject unit in _selectedUnits.Selectables.Values)
                {
                    // Must have a movement component
                    Movement movement = unit.GetComponent<Movement>();
                    if(!movement)
                    {
                        Debug.LogError("MISSING MOVEMENT COMPONENT FOR " + unit.name);
                    }
                    else // then let the character move
                    {
                        movement.SetDestination(Utility.MouseToTerrainPosition());
                    }
                }
            }
        }
    }
}
