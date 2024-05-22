using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReturnToOriginalPosition : MonoBehaviour
{
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private XRGrabInteractable _grabInteractable;

    private void Awake()
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;

        _grabInteractable = GetComponent<XRGrabInteractable>();

        _grabInteractable.selectExited.AddListener((SelectExitEventArgs args) => transform.SetLocalPositionAndRotation(_originalPosition, _originalRotation));
    }
}

