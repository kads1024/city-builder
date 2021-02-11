using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    [Header("Speed Values")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _zoomSpeed;

    [Header("Constraints")]
    [SerializeField] private float _minZoomDistance;
    [SerializeField] private float _maxZoomDistance;

    private Transform _zoomObject;
    private Transform _cameraTransform;

    private void Awake()
    {
        _zoomObject = transform.GetChild(0);
        _cameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        transform.LookAt(_cameraTransform);
    }

    private void Update()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        // Keyboard input
        Vector3 moveInput = _input.CameraDirection();

        // Mouse Position Horizontal
        Vector2 mousePos = Input.mousePosition;
        if (mousePos.x > Screen.width * 0.95f && mousePos.x < Screen.width)
            moveInput.x = 1.0f;
        else if (mousePos.x < Screen.width * 0.05f && mousePos.x > 0.0f)
            moveInput.x = -1.0f;
        // Mouse Position Vertical
        if (mousePos.y > Screen.height * 0.95f && mousePos.y < Screen.height)
            moveInput.z = 1.0f;
        else if (mousePos.y < Screen.height * 0.05f && mousePos.y > 0.0f)
            moveInput.z = -1.0f;

        // Movement
        Vector3 movementDirection = _cameraTransform.TransformDirection(moveInput);
        movementDirection.y = 0;
        transform.position += movementDirection.normalized * Time.deltaTime * _moveSpeed;
    }

    private void Zoom()
    {
        float scrollInput = _input.MouseScroll();

        _zoomObject.localPosition += new Vector3(0, 0, -scrollInput * _zoomSpeed);
        _zoomObject.localPosition = new Vector3(_zoomObject.localPosition.x, _zoomObject.localPosition.y, Mathf.Clamp(_zoomObject.localPosition.z, _minZoomDistance, _maxZoomDistance));
    }
}
