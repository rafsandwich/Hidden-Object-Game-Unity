using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputHandler : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        //only happen when player clicks
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        //check if we are clicking something with an event
        if (!rayHit.collider) return;

        //now we are clicking on an object with a collider
        Debug.Log(rayHit.collider.gameObject.name + " clicked!");
    }

}
