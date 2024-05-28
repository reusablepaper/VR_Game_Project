using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LeftHandController : MonoBehaviour
{

    public InputActionReference openUIReference = null;
    public GameObject menuUI;

    private bool _isUIActive = false;
    private bool _isTriggerPressed = false;
    private bool _wasTriggerPressed = false;

    private void Start()
    {
        menuUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        _isTriggerPressed = openUIReference.action.ReadValue<float>() > 0.1f;
        if (_isTriggerPressed && !_wasTriggerPressed)
        {
            _isUIActive = !_isUIActive;
            menuUI.SetActive(_isUIActive);
        }

        _wasTriggerPressed = _isTriggerPressed;

    }
}
