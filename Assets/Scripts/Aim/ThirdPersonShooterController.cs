using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;

    private StarterAssetsInputs starterAssetsInputs;
    private void Awake()
    {
        GameObject _tempCamera = GameObject.FindGameObjectWithTag("AimingCamera");
        aimVirtualCamera = _tempCamera.GetComponent<CinemachineVirtualCamera>();
        aimVirtualCamera.Follow = GameObject.Find("PlayerCameraRoot").transform;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (starterAssetsInputs.aim) 
        {
            aimVirtualCamera.gameObject.SetActive(true);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
        }
    }
}
