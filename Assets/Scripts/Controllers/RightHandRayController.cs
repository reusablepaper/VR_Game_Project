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
        // ������ ���� ������Ʈ�� �ִ� XRRayInteractor ������Ʈ�� �����ɴϴ�.
        _rayInteractor = GetComponent<XRRayInteractor>();
    }

    private void OnEnable()
    {
        // XRRayInteractor�� Interactor Events�� �̺�Ʈ �ڵ鷯�� �߰��մϴ�.
        _rayInteractor.uiHoverEntered.AddListener(OnUIHoverEntered);
        _rayInteractor.uiHoverExited.AddListener(OnUIHoverExited);
    }

    private void OnDisable()
    {
        // �̺�Ʈ �ڵ鷯�� �����Ͽ� �޸� ������ �����մϴ�.
        _rayInteractor.uiHoverEntered.RemoveListener(OnUIHoverEntered);
        _rayInteractor.uiHoverExited.RemoveListener(OnUIHoverExited);
    }

    private void OnUIHoverEntered(UIHoverEventArgs args)
    {
        // UI�� ȣ������ �� ������ ��ȿ���� Ȱ��ȭ�մϴ�.
        _rayInteractor.allowHover = true;
        _rayInteractor.allowSelect = true;
    }

    private void OnUIHoverExited(UIHoverEventArgs args)
    {
        // UI���� ����� �� ������ ��ȿ���� ��Ȱ��ȭ�մϴ�.
        _rayInteractor.allowHover = false;
        _rayInteractor.allowSelect = false;
    }
}
