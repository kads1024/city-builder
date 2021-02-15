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

    // The List of valid resources
    [SerializeField] private GameObjectRuntimeSet _resourceSet;

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
            else if (_resourceSet.Items.Contains(hitObject.gameObject)) // Check if mouse click hits the a resource
            {
                // Iterate through each unit
                foreach (GameObject unit in _selectedUnits.Selectables.Values)
                {
                    // Must have a attack component
                    ResourceCollector attack = unit.GetComponent<ResourceCollector>();
                    if (!attack)
                    {
                        Debug.LogError("MISSING ATTACK COMPONENT FOR " + unit.name);
                    }
                    else // then let the character move
                    {
                        attack.AttackTarget(hitObject.GetComponent<Damageable>());
                    }
                }
            }
        }
    }
}
