using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class RightHandRayController : MonoBehaviour
{
    private XRRayInteractor _rayInteractor;

    private void Awake()
    {
        // 동일한 게임 오브젝트에 있는 XRRayInteractor 컴포넌트를 가져옵니다.
        _rayInteractor = GetComponent<XRRayInteractor>();
    }

    private void OnEnable()
    {
        // XRRayInteractor의 Interactor Events에 이벤트 핸들러를 추가합니다.
        _rayInteractor.uiHoverEntered.AddListener(OnUIHoverEntered);
        _rayInteractor.uiHoverExited.AddListener(OnUIHoverExited);
    }

    private void OnDisable()
    {
        // 이벤트 핸들러를 제거하여 메모리 누수를 방지합니다.
        _rayInteractor.uiHoverEntered.RemoveListener(OnUIHoverEntered);
        _rayInteractor.uiHoverExited.RemoveListener(OnUIHoverExited);
    }

    private void OnUIHoverEntered(UIHoverEventArgs args)
    {
        // UI에 호버했을 때 레이의 유효성을 활성화합니다.
        _rayInteractor.allowHover = true;
        _rayInteractor.allowSelect = true;
    }

    private void OnUIHoverExited(UIHoverEventArgs args)
    {
        // UI에서 벗어났을 때 레이의 유효성을 비활성화합니다.
        _rayInteractor.allowHover = false;
        _rayInteractor.allowSelect = false;
    }
}
