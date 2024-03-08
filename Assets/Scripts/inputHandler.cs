using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class inputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    //public SpriteRenderer spriteRendererNew;
    //public Sprite newSprite;

    public TextMeshProUGUI numberText;
    public int counter = 2;

    //public bool gameIsRunning = true;

    public ParticleSystem particles;

    public AudioClip soundClip;

    public GameObject winPanel;

    public CameraDragC dragCamScript;

    public float targetCameraSize = 220f;

    private void Awake()
    {
        _mainCamera = Camera.main;
        dragCamScript = Camera.main.GetComponent<CameraDragC>();
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

        particles.transform.position = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        particles.Play();

        GetComponent<AudioSource>().PlayOneShot(soundClip);

        if (counter <= 0)
        {
            winPanel.SetActive(true);
            StartCoroutine(FadeInWinPanel(winPanel, 1f));
        }

        //ChangeSprite(rayHit.collider.GetComponent<SpriteRenderer>().sprite);

    }

    IEnumerator FadeInWinPanel(GameObject panel, float duration)
    {
        // get or add CanvasGroup component to panel so we can fade in parent and children
        CanvasGroup panelCanvasGroup = panel.GetComponent<CanvasGroup>();
        if (panelCanvasGroup == null)
        {
            panelCanvasGroup = panel.AddComponent<CanvasGroup>();
        }

        // fade in the panel
        DisablePlayerInteractions();
        yield return FadeCanvasGroup(panelCanvasGroup, 0f, 1f, duration);
        yield return ZoomOutCamera(_mainCamera, targetCameraSize, 5f);
    }

    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // gradually change alpha value for fade in effect
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ensure final alpha value is set
        canvasGroup.alpha = targetAlpha;
    }

    IEnumerator ZoomOutCamera(Camera camera, float targetSize, float duration)
    {
        float elapsedTime = 0f;
        float startSize = camera.orthographicSize;

        while (elapsedTime < duration)
        {
            camera.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final camera size is set
        camera.orthographicSize = targetSize;
    }


    void DisablePlayerInteractions()
    {
        // Disable the drag camera script
        if (dragCamScript != null)
        {
            dragCamScript.enabled = false;
        }
    }







    /*public void ChangeSprite(Sprite sprite)
    {
        spriteRendererNew.sprite = newSprite;
    }*/


}
