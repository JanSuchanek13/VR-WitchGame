using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;
    public bool leftTeleportEnabled { get; set; } = true;
    public bool rightTeleportEnabled { get; set; } = true;
    private bool leftButtonPressedLastFrame = false;
    private bool rightButtonPressedLastFrame = false;
    public GameObject leftTeleportReticle;
    public GameObject rightTeleportReticle;

    public bool EnableLeftTeleportation { get; set; } = true;
    public bool EnableRightTeleportation { get; set; } = true;

    void Start()
    {
        InitializeTeleportRay(leftTeleportRay);
        InitializeTeleportRay(rightTeleportRay);
    }

    void InitializeTeleportRay(XRController teleportRay)
    {
        if (!teleportRay) { return; }

        teleportRay.gameObject.SetActive(false);
    }

    void Update()
    {
        ManageTeleportRay(leftTeleportRay, ref leftButtonPressedLastFrame, leftTeleportReticle, EnableLeftTeleportation);
        ManageTeleportRay(rightTeleportRay, ref rightButtonPressedLastFrame, rightTeleportReticle, EnableRightTeleportation);
    }

    void ManageTeleportRay(XRController teleportRay, ref bool buttonPressedLastFrame, GameObject teleportReticle, bool teleportEnabled)
    {
        if (!teleportRay) { return; }

        // get the state of the teleport button
        InputHelpers.IsPressed(teleportRay.inputDevice, teleportActivationButton, out bool isPressed, activationThreshold);

        bool buttonJustPressed = isPressed && !buttonPressedLastFrame;
        bool buttonJustReleased = !isPressed && buttonPressedLastFrame;

        if (buttonJustPressed && teleportEnabled)
        {
            teleportRay.gameObject.SetActive(true);
            // this stops the reticle from appearing by the player's feet for 1 frame every time the teleport ray was activated
            teleportReticle.SetActive(false);
        }
        else if (buttonJustReleased)
        {
            // if we disable this object this frame, then the teleport won't work, so do it next frame
            SetActiveNextFrame(teleportRay.gameObject, false);
        }

        buttonPressedLastFrame = isPressed;
    }

    public void SetActiveNextFrame(GameObject gameObject, bool active)
    {
        StartCoroutine(SetActiveNextFrameHelper(gameObject, active));
    }

    IEnumerator SetActiveNextFrameHelper(GameObject gameObject, bool active)
    {
        yield return null; // https://stackoverflow.com/a/39269012/1455558
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(active);
    }
}
