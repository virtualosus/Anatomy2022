using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButtonChecker : MonoBehaviour
{
    public XRController xrController;

    private void Update()
    {
        HandleButtonPresses();
        HandleThumbstickMovement();
    }

    private void HandleButtonPresses()
    {
        foreach (var button in new InputHelpers.Button[]
        {
            InputHelpers.Button.Trigger,
            InputHelpers.Button.Grip,
            InputHelpers.Button.PrimaryButton,
            InputHelpers.Button.SecondaryButton,
            //InputHelpers.Button.PrimaryTouch,
            //InputHelpers.Button.SecondaryTouch,
            InputHelpers.Button.MenuButton
        })
        {
            bool isButtonPressed;
            if (GetButtonValue(button, out isButtonPressed) && isButtonPressed)
            {
                Debug.Log($"{xrController.gameObject.name}: {button} is pressed.");
            }
        }
    }

    private void HandleThumbstickMovement()
    {
        Vector2 thumbstickValue;
        if (xrController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out thumbstickValue))
        {
            Debug.Log($"{xrController.gameObject.name}: Thumbstick value: {thumbstickValue}");
        }
    }

    private bool GetButtonValue(InputHelpers.Button button, out bool value)
    {
        switch (button)
        {
            case InputHelpers.Button.Trigger:
                return xrController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out value);
            case InputHelpers.Button.PrimaryButton:
                return xrController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out value);
            case InputHelpers.Button.GripButton:
                return xrController.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out value);
            case InputHelpers.Button.SecondaryButton:
                return xrController.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out value);
            //case InputHelpers.Button.PrimaryTouch:
            //    return xrController.inputDevice.TryGetFeatureValue(CommonUsages.primaryTouch, out value);
            //case InputHelpers.Button.SecondaryTouch:
            //    return xrController.inputDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out value);
            case InputHelpers.Button.MenuButton:
                return xrController.inputDevice.TryGetFeatureValue(CommonUsages.menuButton, out value);
        }

        value = false;
        return false;
    }
}
