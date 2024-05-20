using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectBoard : MonoBehaviour
{


    public GameObject objectToSpawn; // 생성할 오브젝트의 프리팹
    public Transform spawnPoint; // 오브젝트가 생성될 위치
    public InputActionReference triggerAction = null;

    void OnEnable()
    {
        // 트리거 버튼 입력 이벤트에 메서드 등록
        triggerAction.action.performed += OnTriggerPressed;
        triggerAction.action.Enable();
    }

    void OnDisable()
    {
        // 입력 이벤트에서 메서드 해제
        triggerAction.action.performed -= OnTriggerPressed;
        triggerAction.action.Disable();
    }

    void OnTriggerPressed(InputAction.CallbackContext context)
    {
        Vector3 newPosition = spawnPoint.position + new Vector3(0.0f, 0.0f, -0.3f); // z축으로  이동
        Quaternion newRotation = spawnPoint.rotation * Quaternion.Euler(0.0f, 90.0f, -90.0f); 

        // 트리거 버튼이 눌리면 오브젝트 생성
        GameObject board =Instantiate(objectToSpawn, newPosition, newRotation);
        
        board.SetActive(true);

    }
}
