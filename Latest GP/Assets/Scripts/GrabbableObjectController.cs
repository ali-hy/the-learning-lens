using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GrabbableObjectController : MonoBehaviour
{
    [Header("Components")]
    public ObjectInfoPanel infoPanel;


    [Header("Panel Display Settings")]
    public float panelDisplayDuration = 10f;

    private Coroutine hideCoroutine;

    private InteractableObjectInfo objectInfo;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        // Get required components
        objectInfo = GetComponent<InteractableObjectInfo>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Find info panel if not assigned
        if (infoPanel == null)
            infoPanel = GetComponentInChildren<ObjectInfoPanel>();

        // Validate setup
        if (objectInfo == null)
        {
            Debug.LogError($"InteractableObjectInfo component missing on {gameObject.name}");
        }

        if (infoPanel == null)
        {
            Debug.LogError($"ObjectInfoPanel not found on {gameObject.name}");
        }

        if (grabInteractable == null)
        {
            Debug.LogError($"XRGrabInteractable component missing on {gameObject.name}");
        }
    }

    void Start()
    {
        // Subscribe to grab events
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnObjectGrabbed);
            grabInteractable.selectExited.AddListener(OnObjectReleased);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from events
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnObjectGrabbed);
            grabInteractable.selectExited.RemoveListener(OnObjectReleased);
        }
    }

    void OnObjectGrabbed(SelectEnterEventArgs args)
    {
        ShowInfoPanel();
    }

    void OnObjectReleased(SelectExitEventArgs args)
    {
        // Do nothing - let the timer handle hiding
    }


    System.Collections.IEnumerator DelayedHidePanel()
    {
        yield return new WaitForSeconds(0.1f);
        HideInfoPanel();
    }

    // Public methods that can also be called from UnityEvents in inspector
    public void ShowInfoPanel()
    {
        if (infoPanel != null && objectInfo != null)
        {
            // Stop any existing hide timer
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }

            // Show panel
            infoPanel.ShowPanel(objectInfo.GetInfo());

            // Start timer to hide panel
            hideCoroutine = StartCoroutine(HideAfterDelay());
        }
    }
    System.Collections.IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(panelDisplayDuration);
        HideInfoPanel();
        hideCoroutine = null;
    }

    public void HideInfoPanel()
    {
        if (infoPanel != null)
        {
            infoPanel.HidePanel();
        }
    }

    // Optional: Method to update info at runtime
    public void UpdateObjectInfo(ObjectInfo newInfo)
    {
        if (objectInfo != null)
        {
            objectInfo.objectInfo = newInfo;
        }
    }


    // Method to force show panel even when socketed (for special cases)
    public void ForceShowInfoPanel()
    {
        if (infoPanel != null && objectInfo != null)
        {
            infoPanel.ShowPanel(objectInfo.GetInfo());
        }
    }


}