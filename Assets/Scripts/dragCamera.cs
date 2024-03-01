using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class dragCamera : MonoBehaviour
{
    private Vector3 _origin;
    private Vector3 _difference;
    
    private Camera _mainCamera;

    private bool _isDragging;

    private Bounds _cameraBounds;
    private Vector3 _targetPosition;


    private void Awake()
    {
        _mainCamera = Camera.main;

    }

    public void onDrag(InputAction.CallbackContext ctx)
    {
        //set our origin
        //set value of isDragging depending on whether RMB is being held
        //check state of action, started / performed / cancelled
        //otherwise it will be false, so not listening to if it's cancelled

        if (ctx.started) _origin = GetMousePosition;
        _isDragging = ctx.started || ctx.performed;

    }

    private void Start()
    {
        //get height & width of camera
        var height = _mainCamera.orthographicSize;
        var width = height * _mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        //set camera bounds
        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0),
            new Vector3(maxX, maxY, 0)
        );
    }

    //take care of camera movement
    private void LateUpdate()
    {
        //are we dragging?
        if (!_isDragging) return;

        _difference = GetMousePosition - transform.position;

        _targetPosition = _origin - _difference;
        _targetPosition = GetCameraBounds();

        //set position of camera
        transform.position = _targetPosition;
    }

    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(_targetPosition.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(_targetPosition.y, _cameraBounds.min.y, _cameraBounds.max.y),
            transform.position.z
        );
    }

    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}
