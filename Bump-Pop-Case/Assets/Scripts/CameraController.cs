using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera c_VirtualCamera;
    [SerializeField] Transform target;

    void Awake()
    {
        c_VirtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    void Start()
    {
        c_VirtualCamera.m_LookAt = target.transform;
    }

    public void ChangeLookTarget(Transform point)
    {
        c_VirtualCamera.m_LookAt = point;
    }

}
