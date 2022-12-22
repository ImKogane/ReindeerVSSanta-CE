using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class ThirdPersonShooterController : NetworkBehaviour
{
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private float shootCooldown;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] public Transform bulletProjectile;
    [SerializeField] private Sprite crosshairNormal;
    [SerializeField] private Sprite crosshairAim;

    private Transform debugTransform;
    private Transform spawnBulletPosition;
    private CinemachineVirtualCamera aimVirtualCamera;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private bool canShoot;
    private void Awake()
    {
        GameObject _tempCamera = GameObject.FindGameObjectWithTag("AimingCamera");
        aimVirtualCamera = _tempCamera.GetComponent<CinemachineVirtualCamera>();
        aimVirtualCamera.Follow = GameObject.Find("PlayerCameraRoot").transform;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        debugTransform = GameObject.FindGameObjectWithTag("AimPoint").transform;
        spawnBulletPosition = GameObject.FindGameObjectWithTag("FirePosition").transform;
        shootCooldown = 0.8f;
        canShoot = false;
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (starterAssetsInputs.aim) 
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);

            GameObject _tempUI = GameObject.FindGameObjectWithTag("Crosshair");
            Image _tempImage = _tempUI.GetComponent<Image>();
            _tempImage.sprite = crosshairAim;

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            canShoot = true;

            if (shootCooldown > 0)
            {
                shootCooldown -= Time.deltaTime;
            }
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);

            GameObject _tempUI = GameObject.FindGameObjectWithTag("Crosshair");
            Image _tempImage = _tempUI.GetComponent<Image>();
            _tempImage.sprite = crosshairNormal;

            canShoot = false;

            shootCooldown = 0.8f;
        }

        if (starterAssetsInputs.shoot)
        {
            if (shootCooldown <= 0)
            {
                if(canShoot == true)
                {
                    Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                    Instantiate(bulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                    shootCooldown = 0.8f;
                    starterAssetsInputs.shoot = false;
                }
            }
        }
    }
}
