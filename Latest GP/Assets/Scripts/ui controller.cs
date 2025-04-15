using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class uicontroller : MonoBehaviour
{

    [SerializeField] private XRRayInteractor xrRayInteractor;
    [SerializeField] private ActionBasedController actionBasedController;
    [SerializeField] private XRDirectInteractor XRDirectInteractor;
    [SerializeField] private InputActionReference uiActionReference;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        uiActionReference.action.performed += UIModeActivate;

    }

    private void UIModeActivate(InputAction.CallbackContext obj)
    {
        XRDirectInteractor.enabled = false;

        xrRayInteractor.enabled = true;
        actionBasedController.enableInputActions = true;
    }

    private void UIModeCancel(in InputAction.CallbackContext obj) => Invoke("DisableUI", 0.05f);

    private void DisableUI(in InputAction.CallbackContext obj)
    {
        XRDirectInteractor.enabled = true;

        xrRayInteractor.enabled = false;
        actionBasedController.enableInputActions = false;
    }
    private void OnDisable()
    {
        uiActionReference.action.performed -= UIModeActivate;

    }
}
