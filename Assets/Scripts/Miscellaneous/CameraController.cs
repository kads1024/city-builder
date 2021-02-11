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

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        Vector3 direction = _input.CameraDirection();
        transform.position += Time.deltaTime * _moveSpeed * direction;
    }

    private void Zoom()
    {
        float scrollInput = _input.MouseScroll();
        float cameraDistance = Vector3.Distance(transform.position, _camera.transform.position);

        if (cameraDistance <= _minZoomDistance && scrollInput > 0.0f)
            return;
        else if (cameraDistance >= _maxZoomDistance && scrollInput < 0.0f)
            return;

        _camera.transform.position += _camera.transform.forward * scrollInput * _zoomSpeed;
    }
}
