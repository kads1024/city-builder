using UnityEngine;
using DG.Tweening;

/// <summary>
/// The component of the object that will be considered as a building
/// </summary>
public class Building : MonoBehaviour
{
    [Header("Building Info")]
    [SerializeField] private string _buildingName;
    [SerializeField] private Cost _resourceCost;
    [SerializeField] private float _totalWorkToComplete = 100f;
    private float _currentWork;

    [Header("Building Parameters")]
    [SerializeField] private float _height;
    [SerializeField] private float _radius = 5;
    private float _originalHeight;

    // The Building Object Itself
    private Transform _buildingTransform;
    private MeshRenderer _buildingRenderer;
    private bool _doneBuilding;

    // Visuals
    [ColorUsage(true, true)]
    [SerializeField] private Color[] _stateColors;
    Cinemachine.CinemachineImpulseSource _impulseSource;


    private void Awake()
    {
        _impulseSource = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }

    void Start()
    {
        // Get the building transform
        _buildingTransform = transform.GetChild(0);
        _buildingRenderer = _buildingTransform.GetComponent<MeshRenderer>();

        // Reset the current work
        _currentWork = 0;
        _doneBuilding = false;

        // Set up initial position for building the object
        _originalHeight = _buildingTransform.localPosition.y;
        _buildingTransform.localPosition = Vector3.down * _height;
    }

    /// <summary>
    /// Build the current building by a certain amount
    /// </summary>
    /// <param name="p_workAmount">The amount of progress/work to put into a building</param>
    public void Build(float p_workAmount)
    {
        // Add the work to the current progress of the building
        _currentWork += p_workAmount;

        // As a visual, Interpolate the initial position to the original position (above ground)
        _buildingTransform.localPosition = Vector3.Lerp(Vector3.down * _height, new Vector3(0, _originalHeight, 0), _currentWork / _totalWorkToComplete);

        // Visual effect for each build iteration
        _buildingTransform.DOComplete();
        _buildingTransform.DOShakeScale(.5f, .2f, 10, 90, true);
        //BuildingManager.instance.PlayParticle(transform.position);
    }

    /// <summary>
    /// If the current building is finished
    /// </summary>
    /// <returns>Whether or not the building is finished</returns>
    public bool IsFinished()
    {
        
        if (_currentWork >= _totalWorkToComplete && !_doneBuilding && _buildingRenderer)
        {
            // Set done building
            _doneBuilding = true;

            // Flash the state color  for 0.1f seconds, then reset the color back after
            _buildingRenderer.material.DOColor(_stateColors[1], "Color_D2F0A66D", .1f).
                OnComplete(() => _buildingRenderer.material.DOColor(_stateColors[0], "Color_D2F0A66D", .5f));

            // Generate Impulse for camera if any
            if (_impulseSource)
                _impulseSource.GenerateImpulse();
        }
        return _currentWork >= _totalWorkToComplete;
    }

    /// <summary>
    /// Since the cost is a private variable, we still need to have access to it
    /// </summary>
    /// <returns>The cost of this building  </returns>
    public Cost GetCost()
    {
        return _resourceCost;
    }

    /// <summary>
    /// Since the radius is a private variable, we still need to have access to it
    /// </summary>
    /// <returns>The radius of this building  </returns>
    public float GetRadius()
    {
        return _radius;
    }

    /// <summary>
    /// Visual to draw building area
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);

        Gizmos.DrawLine(transform.position, transform.position + (transform.up * _height));
    }
}

[System.Serializable]
public struct Cost
{
    public ResourceType Resource;
    public int Amount;
}