using System.Collections;
using UnityEngine;
using TMPro;

public class VRKeyboardManager : MonoBehaviour
{
    private TouchScreenKeyboard keyboard;
    private TMP_InputField activeInputField;

    private void OnEnable()
    {
        // Automatically register for selection events on all TMP_InputFields in the scene
        TMP_InputField[] allInputs = FindObjectsByType<TMP_InputField>(FindObjectsSortMode.None);
        foreach (var input in allInputs)
        {
            input.onSelect.AddListener(OnInputFieldSelected);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        TMP_InputField[] allInputs = FindObjectsByType<TMP_InputField>(FindObjectsSortMode.None);
        foreach (var input in allInputs)
        {
            input.onSelect.RemoveListener(OnInputFieldSelected);
        }
    }

    private void OnInputFieldSelected(string _)
    {
        // Find which InputField was selected
        activeInputField = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject?.GetComponent<TMP_InputField>();
        if (activeInputField == null) return;

        keyboard = TouchScreenKeyboard.Open(activeInputField.text, TouchScreenKeyboardType.Default);
        StartCoroutine(UpdateInputFieldText());
    }

    private IEnumerator UpdateInputFieldText()
    {
        while (keyboard != null && keyboard.status != TouchScreenKeyboard.Status.Done)
        {
            if (activeInputField != null)
                activeInputField.text = keyboard.text;
            yield return null;
        }

        if (activeInputField != null && keyboard != null)
            activeInputField.text = keyboard.text;
    }
}
