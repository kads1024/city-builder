using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// The component of the object that will be considered as a building
/// </summary>
public class Building : MonoBehaviour
{
    // BuildingController to be used
    [SerializeField] private BuildingController _buildingController;

    [Header("Building Info")]
    [SerializeField] private string _buildingName;
    [SerializeField] private List<Cost> _resourceCost;
    [SerializeField] private float _totalWorkToComplete = 100f;
    private float _currentWork;
    
    [Header("Building Parameters")]
    [SerializeField] private float _height;
    [SerializeField] private float _radius = 5;
    [SerializeField] private Vector3 _positionOffset;
    private float _originalHeight;

    // The Building Object Itself
    private Rigidbody _rigidbody;
    private Transform _buildingTransform;
    private MeshRenderer _buildingRenderer;
    private bool _doneBuilding;

    // Visuals
    [ColorUsage(true, true)]
    [SerializeField] private Color[] _stateColors;
    [SerializeField] private ParticleSystem _buildParticle;
    private Cinemachine.CinemachineImpulseSource _impulseSource;

    // Events to be fired when the building has finished constructing
    private UnityEvent _onFinishBuild;

    private void Awake()
    {
        _impulseSource = GetComponent<Cinemachine.CinemachineImpulseSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _onFinishBuild = new UnityEvent();
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
        PlayParticle(transform.position);
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
            _buildingRenderer.material.DOColor(_stateColors[1], Constants.EMISSION_COLOR, .1f).
                OnComplete(() => _buildingRenderer.material.DOColor(_stateColors[0], Constants.EMISSION_COLOR, .5f));

            // Generate Impulse for camera if any
            if (_impulseSource)
                _impulseSource.GenerateImpulse();

            // Remove Rigid body if it has one
            if (_rigidbody)
                Destroy(_rigidbody);

            // Fires the on finish build event
            _onFinishBuild.Invoke();
        }
        return _currentWork >= _totalWorkToComplete;
    }

    /// <summary>
    /// Used to play the particle while building
    /// </summary>
    /// <param name="p_position">The position where to place the particle</param>
    private void PlayParticle(Vector3 p_position)
    {
        if (_buildParticle)
        {
            ParticleSystem particle = Instantiate(_buildParticle, p_position, Quaternion.identity);
            Destroy(particle.gameObject, 0.3f);
        }
    }

    /// <summary>
    /// Since the cost is a private variable, we still need to have access to it
    /// </summary>
    /// <returns>The cost of this building  </returns>
    public List<Cost> GetCost()
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
    /// Gets the offset position of the object
    /// </summary>
    /// <returns>The position offset of the object</returns>
    public Vector3 GetPositionOffset()
    {
        return _positionOffset;
    }

    /// <summary>
    /// Register a listener to the on finish building event
    /// </summary>
    /// <param name="p_callback">The Call back to be registered</param>
    public void RegisterOnFinishListener(UnityAction p_callback)
    {
        _onFinishBuild.AddListener(p_callback);
    }

    /// <summary>
    /// Unregisters a listener to the on finish building event
    /// </summary>
    /// <param name="p_callback">The Call back to be removed</param>
    public void UnregisterOnFinishListener(UnityAction p_callback)
    {
        _onFinishBuild.RemoveListener(p_callback);
    }

    /// <summary>
    /// Visual to draw building area
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + _positionOffset, _radius);

        Gizmos.DrawLine(transform.position, transform.position + (transform.up * _height));
    }
}

[System.Serializable]
public struct Cost
{
    public ResourceType Resource;
    public int Amount;
}