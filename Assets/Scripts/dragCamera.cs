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

    //take care of camera movement
    private void LateUpdate()
    {
        //are we dragging?
        if (!_isDragging) return;

        _difference = GetMousePosition - transform.position;

        //set position of camera
        transform.position = _origin - _difference;
    }

    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}
