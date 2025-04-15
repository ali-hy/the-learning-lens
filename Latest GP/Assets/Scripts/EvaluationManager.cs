using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UI;
using TMPro;

public class EvaluationManager : MonoBehaviour
{
    // Total number of sockets/parts in the complete model
    [SerializeField] private int totalParts = 0;

    // Current number of correctly placed parts
    private int correctlyPlacedParts = 0;

    // Optional: Reference to a UI Text or slider to display progress
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Slider progressSlider;

    // Dictionary to track which sockets have correct parts
    private Dictionary<string, bool> socketStatus = new Dictionary<string, bool>();

    void Start()
    {
        // Count total parts if not set in inspector
        if (totalParts <= 0)
        {
            // Find all socket interactors in the scene
            XRSocketInteractor[] allSockets = FindObjectsOfType<XRSocketInteractor>();
            totalParts = allSockets.Length;

            // Initialize the dictionary
            foreach (XRSocketInteractor socket in allSockets)
            {
                socketStatus.Add(socket.gameObject.name, false);
            }
        }

        UpdateProgressDisplay();
    }

    public void OnObjectSocketed(SelectEnterEventArgs args)
    {
        // Get the interactable that was placed
        XRBaseInteractable interactable = args.interactableObject.transform.GetComponent<XRBaseInteractable>();
        string objectName = interactable.gameObject.name;

        // Get the socket
        XRSocketInteractor socketInteractor = args.interactorObject.transform.GetComponent<XRSocketInteractor>();
        string socketName = socketInteractor.gameObject.name;

        // Check if this is a correct placement
        bool isCorrectPlacement = CheckCorrectPlacement(socketName, objectName);

        // Update dictionary and progress
        if (isCorrectPlacement && !socketStatus[socketName])
        {
            socketStatus[socketName] = true;
            correctlyPlacedParts++;
            UpdateProgressDisplay();

            Debug.Log("Correct placement: " + objectName + " in " + socketName);
            Debug.Log("Progress: " + GetProgressPercentage() + "%");
        }
    }

    public void OnObjectUnsocketed(SelectExitEventArgs args)
    {
        // Get the socket
        XRSocketInteractor socketInteractor = args.interactorObject.transform.GetComponent<XRSocketInteractor>();
        string socketName = socketInteractor.gameObject.name;

        // If this socket had a correct part, decrement the counter
        if (socketStatus[socketName])
        {
            socketStatus[socketName] = false;
            correctlyPlacedParts--;
            UpdateProgressDisplay();

            Debug.Log("Correct part removed from " + socketName);
            Debug.Log("Progress: " + GetProgressPercentage() + "%");
        }
    }

    private bool CheckCorrectPlacement(string socketName, string objectName)
    {
        // Remove the word "socket" from the socket name
        string expectedPartName = socketName.Replace(" socket", "").Replace("Socket", "");

        // Check if the object name matches the expected part name
        return objectName.Equals(expectedPartName, System.StringComparison.OrdinalIgnoreCase);
    }

    private float GetProgressPercentage()
    {
        return (float)correctlyPlacedParts / totalParts * 100f;
    }

    private void UpdateProgressDisplay()
    {
        float progressPercentage = GetProgressPercentage();

        // Update UI elements if they exist
        if (progressText != null)
        {
            progressText.text = "Building Progress: " + progressPercentage.ToString("F1") + "%";
        }

        if (progressSlider != null)
        {
            progressSlider.value = progressPercentage / 100f;
        }
    }

    // Public method to check if model is complete
    public bool IsModelComplete()
    {
        return correctlyPlacedParts >= totalParts;
    }
}