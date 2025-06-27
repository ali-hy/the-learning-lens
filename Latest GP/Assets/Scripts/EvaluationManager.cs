using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
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

    // Reference to TMP texts and slider to display progress
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI instructionsText;
    [SerializeField] private Slider progressSlider;

    // Dictionary to track which sockets have correct parts
    private Dictionary<string, bool> socketStatus = new();

    // List of part names in proper installation order (if you want a specific order)
    [SerializeField] private List<string> partsInOrder = new List<string>();

    // Flag to track if we auto-discovered parts or used the provided list
    private bool usedProvidedPartsList = false;

    [SerializeField] private AudioSource positiveAudio;
    [SerializeField] private AudioSource negativeAudio;
    [SerializeField] private AudioSource endingAudio;
    [SerializeField] private GameObject particleSystem;

    void Start()
    {
        // Check if we have a predefined list of parts
        usedProvidedPartsList = partsInOrder.Count > 0;

        // Count total parts if not set in inspector
        if (totalParts <= 0 || !usedProvidedPartsList)
        {
            OnModelLoaded();
        }
        else
        {
            // Initialize the dictionary with the predefined parts
            XRSocketInteractor[] allSockets = FindObjectsOfType<XRSocketInteractor>();
            foreach (XRSocketInteractor socket in allSockets)
            {
                socketStatus.Add(socket.gameObject.name, false);
            }
        }

        UpdateProgressDisplay();
        UpdateInstructionsDisplay();
    }

    public void OnModelLoaded()
    {
        // Find all socket interactors in the scene
        XRSocketInteractor[] allSockets = FindObjectsByType<XRSocketInteractor>(FindObjectsSortMode.None);

        if (totalParts <= 0)
            totalParts = allSockets.Length;

        // Initialize the dictionary and part list if needed
        foreach (XRSocketInteractor socket in allSockets)
        {
            string socketName = socket.gameObject.name;
            socketStatus.Add(socketName, false);

            if (!usedProvidedPartsList)
            {
                // Extract expected part name from socket name
                string partName = GetExpectedPartName(socketName);
                if (!string.IsNullOrEmpty(partName))
                {
                    partsInOrder.Add(partName);
                }
            }
        }

        UpdateProgressDisplay();
        UpdateInstructionsDisplay();
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
            UpdateInstructionsDisplay();
            positiveAudio.Play();
            Debug.Log("Correct placement: " + objectName + " in " + socketName);
            Debug.Log("Progress: " + GetProgressPercentage() + "%");
            //play ending effects
            if(IsModelComplete())
            {
                endingAudio.Play();
                particleSystem.SetActive(true);
            }
        }
        else
        {
            negativeAudio.Play();
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
            UpdateInstructionsDisplay();
            negativeAudio.Play();
            Debug.Log("Correct part removed from " + socketName);
            Debug.Log("Progress: " + GetProgressPercentage() + "%");
        }
    }

    private bool CheckCorrectPlacement(string socketName, string objectName)
    {
        // Get the expected part name
        string expectedPartName = GetExpectedPartName(socketName);

        // Check if the object name matches the expected part name
        return objectName.Equals(expectedPartName, System.StringComparison.OrdinalIgnoreCase);
    }

    private string GetExpectedPartName(string socketName)
    {
        // Remove the word "socket" from the socket name
        return socketName.Replace(" socket", "").Replace("Socket", "").Trim();
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

    private void UpdateInstructionsDisplay()
    {
        if (instructionsText == null)
            return;

        // Create rich text string for instructions
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.AppendLine("<b>Assembly Instructions:</b>");
        sb.AppendLine();

        // Go through each part in the list
        foreach (string partName in partsInOrder)
        {
            bool isInstalled = false;

            // Check if this part is already installed
            foreach (KeyValuePair<string, bool> entry in socketStatus)
            {
                string expectedPartName = GetExpectedPartName(entry.Key);
                if (expectedPartName.Equals(partName, System.StringComparison.OrdinalIgnoreCase) && entry.Value)
                {
                    isInstalled = true;
                    break;
                }
            }

            // Format the part name based on installation status
            if (isInstalled)
            {
                // Use strikethrough and darker color for installed parts
                sb.AppendLine("<s><color=#8C8C8C>" + partName + "</color></s> [DONE]");
            }
            else
            {
                // Use normal bright text for parts still to be installed
                sb.AppendLine("<color=#FFFFFF>" + partName + "</color>");
            }
        }

        // Update the instructions text
        instructionsText.text = sb.ToString();
    }

    public bool IsModelComplete()
    {
        return correctlyPlacedParts >= totalParts;
    }

    public bool OnModelComplete()
    {
        if (IsModelComplete())
        {
            Debug.Log("Model assembly complete!");
            // Optionally trigger any completion events or UI updates here
            return true;
        }
        else
        {
            Debug.Log("Model assembly not complete yet.");
            return false;
        }
    }
}