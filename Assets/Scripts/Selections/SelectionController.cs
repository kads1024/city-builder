using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    [SerializeField] private GameObjectRuntimeSet _selectableUnitSet;

    [SerializeField] private PlayerInput _input;
    [SerializeField] private SelectableContainer _selectableContainer;

    private Camera _camera;

    private Vector3 _mouseStartPosition;
    private Vector3 _mouseEndPosition;

    private bool _dragging;

    private MeshCollider _selectionBox;
    private Mesh _selectionMesh;

    //the corners of our 2d selection box
    private Vector2[] _corners;

    //the vertices of our meshcollider
    private Vector3[] _vertices;

    private void Awake()
    {
        _camera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        _dragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Record mouse position when it is first clicked
        if(_input.OnSelectionClick())
        {
            _mouseStartPosition = Input.mousePosition;
        }

        // Only set drag when the mouse travels at a certain distance
        if(_input.OnSelectionHold())
        {
            float mouseTravelDistance = (_mouseStartPosition - Input.mousePosition).magnitude;
            if(mouseTravelDistance > 40)
            {
                _dragging = true;
            }
        } 

        // When the selection button is released,
        if(_input.OnSelectionUp())
        {
            // If you click without dragging, select only the unit that is clicked
            if(!_dragging)
            {
                Collider clickedObject = Utility.MouseToObject().collider;

                // Check if you clicked an object and it is selectable
                if (clickedObject && _selectableUnitSet.Items.Contains(clickedObject.gameObject))
                {
                    // If the player wants multiple selection, add the object to the list without deselecting anything
                    if (_input.MultipleSelectionActive())
                    {
                        _selectableContainer.Add(clickedObject.gameObject);
                    }
                    else // If player doesnt want multiple selection, unselect all and just select the object clicked
                    {                  
                        _selectableContainer.DeselectAll();
                        _selectableContainer.Add(clickedObject.gameObject);
                    }         
                }
                else // If nothing was clicked or object was not selectable
                {
                    // If the player wants multiple selection and object is not selectable, do not deselect anything
                    // If the player doesnt want multiple selection, that is when we will deselect everything
                    if (!_input.MultipleSelectionActive())
                    {
                        _selectableContainer.DeselectAll();                 
                    }
                }
            }
            else // If you click while dragging, grab all units that were selected
            {
                _vertices = new Vector3[4];
                _mouseEndPosition = Input.mousePosition;
                _corners = GetBoundingBox(_mouseStartPosition, _mouseEndPosition);

                int i = 0;
                foreach (Vector2 corner in _corners)
                {
                    Ray ray = _camera.ScreenPointToRay(corner);
                    if (Physics.Raycast(ray, out RaycastHit hit, 50000.0f, LayerMask.GetMask("Ground")))
                    {
                        _vertices[i] = new Vector3(hit.point.x, 0.0f, hit.point.z);
                        Debug.DrawLine(_camera.ScreenToWorldPoint(corner), hit.point, Color.red, 1.0f);
                    }
                    i++;
                }

                // Generate the mesh
                _selectionMesh = GenerateSelectionMesh(_vertices);

                _selectionBox = gameObject.AddComponent<MeshCollider>();
                _selectionBox.sharedMesh = _selectionMesh;
                _selectionBox.convex = true;
                _selectionBox.isTrigger = true;

                // Deselect Everything if player is not multiple Selecting
                if (!_input.MultipleSelectionActive())
                {
                    _selectableContainer.DeselectAll();
                }

                Destroy(_selectionBox, Time.deltaTime * 5f);
            }

            _dragging = false;
        }
    }

    /// <summary>
    /// Create a bounding box (4 corners in order) from the start and end mouse position
    /// </summary>
    /// <param name="p_position1">Start Position</param>
    /// <param name="p_position2">End Position</param>
    /// <returns>The bounding box vertices (in view point) represented by 4 corners</returns>
    private Vector2[] GetBoundingBox(Vector2 p_position1, Vector2 p_position2)
    {
        Vector2 boxPosition1;
        Vector2 boxPosition2;
        Vector2 boxPosition3;
        Vector2 boxPosition4;

        if (p_position1.x < p_position2.x) //if p_position1 is to the left of p_position2
        {
            if (p_position1.y > p_position2.y) // if p_position1 is above p_position2
            {
                boxPosition1 = p_position1;
                boxPosition2 = new Vector2(p_position2.x, p_position1.y);
                boxPosition3 = new Vector2(p_position1.x, p_position2.y);
                boxPosition4 = p_position2;
            }
            else //if p_position1 is below p_position2
            {
                boxPosition1 = new Vector2(p_position1.x, p_position2.y);
                boxPosition2 = p_position2;
                boxPosition3 = p_position1;
                boxPosition4 = new Vector2(p_position2.x, p_position1.y);
            }
        }
        else //if p_position1 is to the right of p_position2
        {
            if (p_position1.y > p_position2.y) // if p_position1 is above p_position2
            {
                boxPosition1 = new Vector2(p_position2.x, p_position1.y);
                boxPosition2 = p_position1;
                boxPosition3 = p_position2;
                boxPosition4 = new Vector2(p_position1.x, p_position2.y);
            }
            else //if p_position1 is below p_position2
            {
                boxPosition1 = p_position2;
                boxPosition2 = new Vector2(p_position1.x, p_position2.y);
                boxPosition3 = new Vector2(p_position2.x, p_position1.y);
                boxPosition4 = p_position1;
            }

        }

        Vector2[] corners = { boxPosition1, boxPosition2, boxPosition3, boxPosition4 };
        return corners;
    }

    // Generate a mesh from the 4 bottom points
    private Mesh GenerateSelectionMesh(Vector3[] p_corners)
    {
        Vector3[] vertices = new Vector3[8];
        int[] triangles = {
            0, 1, 2,
            2, 1, 3,

            4, 6, 0,
            0, 6, 2,

            6, 7, 2,
            2, 7, 3,

            7, 5, 3,
            3, 5, 1,

            5, 0, 1,
            4, 0, 5,

            4, 5, 6,
            6, 5, 7 }; //map the tris of our cube

        // Bottom Rectangle
        for (int i = 0; i < 4; i++)
        {
            vertices[i] = p_corners[i];
        }

        // Top Rectangle
        for (int j = 4; j < 8; j++)
        {
            vertices[j] = p_corners[j - 4] + Vector3.up * 100.0f;
        }

        // Generated Mesh
        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = vertices;
        selectionMesh.triangles = triangles;

        return selectionMesh;
    }

    // When the selection box triggers the game object, add it if it is selectable
    private void OnTriggerEnter(Collider other)
    {
        if (_selectableUnitSet.Items.Contains(other.gameObject))
        {
            _selectableContainer.Add(other.gameObject);
        }      
    }

    /// <summary>
    /// For visualization and debugging purposes
    /// </summary>
    private void OnGUI()
    {
        if(_dragging)
        {
            Rect rect = Utility.GetScreenRect(_mouseStartPosition, Input.mousePosition);
            Utility.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utility.DrawScreenRectBorder(rect, 2.0f, new Color(0.8f, 0.8f, 0.95f));
        }
    }
}
