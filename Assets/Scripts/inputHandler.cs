using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class inputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    //public SpriteRenderer spriteRendererNew;
    //public Sprite newSprite;

    public TextMeshProUGUI numberText;
    int counter = 2;

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
        // Debug.Log(rayHit.collider.gameObject.name + " clicked!");

        //Color myColour = new Color(255, 218, 250, 1); i'm not sure why, but doesn't update colour visually but it recognises colour has 'changed'
        Color myColour = new Vector4(1f, 0.85f, 0.98f, 1f); //hex ffdafa is the target

        if (rayHit.collider.GetComponent<SpriteRenderer>().color == myColour)
        {
            print(rayHit.collider.gameObject.name + " colour already changed!");
            return;
        }

        rayHit.collider.GetComponent<SpriteRenderer>().color = myColour;
        print(rayHit.collider.gameObject.name + " colour changed but smartly! ");
        counter--;
        numberText.text = counter + "";

        //ChangeSprite(rayHit.collider.GetComponent<SpriteRenderer>().sprite);

    }

    /*public void ChangeSprite(Sprite sprite)
    {
        spriteRendererNew.sprite = newSprite;
    }*/

}
