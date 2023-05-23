using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class TouchController : MonoBehaviour
{
    public GameObject sprayPart;
    public GameObject triggerObj;
    //public Button sprayButton;
    public UIDocument gameUIDocument;

    private bool isEmitting = false;

    void Start()
    {
        sprayPart.SetActive(true);
        sprayPart.GetComponent<ParticleSystem>().Stop();

        gameUIDocument = GetComponent<UIDocument>();
        var rootVisualElement = gameUIDocument.rootVisualElement;

        // Find the button using its name or other selector
        Button sprayButton = rootVisualElement.Q<Button>("sprayButton");

        // Attach a click event listener to your button
        sprayButton.clicked += () => OnButtonClick();
    }

    void Update()
    {
        var partSystem = sprayPart.GetComponent<ParticleSystem>();

        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            if (
                Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began
                || Input.GetMouseButtonDown(0)
            )
            {
                Vector3 inputPosition =
                    Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                ProcessTouchInput(inputPosition);
                //partSystem.Play(); // Start particle emission
            }
            else if (
                Input.touchCount > 0
                && (
                    Input.GetTouch(0).phase == TouchPhase.Ended
                    || Input.GetTouch(0).phase == TouchPhase.Canceled
                )
            )
            {
                partSystem.Stop(); // Stop particle emission
            }
        }
        else
        {
            partSystem.Stop(); // Stop particle emission
        }

        if (Input.GetMouseButtonUp(0) && isEmitting)
        {
            Debug.Log("mouse button let go & button clicked");
            triggerObj.transform.Rotate(0f, 0f, 8.5f);
            isEmitting = false;
        }

    }

    void ProcessTouchInput(Vector3 position)
    {
        var partSystem = sprayPart.GetComponent<ParticleSystem>();
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = position;
        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, results);

        if (results.Count > 0)
        {
            isEmitting = true;
            //@Dev
            //Right here is where you will determine what action to take depending on which button was pressed
            //move partSystem.Play() to an if clause that checks if the "sprayButton" button was clicked
            partSystem.Play();
            triggerObj.transform.Rotate(0f, 0f, -8.5f);
            foreach (var result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
            }
        }

    }

    void OnButtonClick()
    {
        Vector3 inputPosition =
                    Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
                ProcessTouchInput(inputPosition);

        isEmitting = true; // Toggle the emission state
        var partSystem = sprayPart.GetComponent<ParticleSystem>();

        if (!isEmitting)
        {
            partSystem.Stop(); // Stop particle emission
        }
        else
        {
            partSystem.Play();
        }
    }
}
