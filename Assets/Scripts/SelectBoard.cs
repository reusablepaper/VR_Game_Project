using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectBoard : MonoBehaviour
{


    public GameObject objectToSpawn; // ������ ������Ʈ�� ������
    public Transform spawnPoint; // ������Ʈ�� ������ ��ġ
    public InputActionReference triggerAction = null;

    void OnEnable()
    {
        // Ʈ���� ��ư �Է� �̺�Ʈ�� �޼��� ���
        triggerAction.action.performed += OnTriggerPressed;
        triggerAction.action.Enable();
    }

    void OnDisable()
    {
        // �Է� �̺�Ʈ���� �޼��� ����
        triggerAction.action.performed -= OnTriggerPressed;
        triggerAction.action.Disable();
    }

    void OnTriggerPressed(InputAction.CallbackContext context)
    {
        Vector3 newPosition = spawnPoint.position + new Vector3(0.0f, 0.0f, -0.3f); // z������  �̵�
        Quaternion newRotation = spawnPoint.rotation * Quaternion.Euler(0.0f, 90.0f, -90.0f); 

        // Ʈ���� ��ư�� ������ ������Ʈ ����
        GameObject board =Instantiate(objectToSpawn, newPosition, newRotation);
        
        board.SetActive(true);

    }
}
