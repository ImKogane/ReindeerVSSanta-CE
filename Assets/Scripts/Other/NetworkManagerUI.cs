using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class NetworkManagerUI : MonoBehaviour
{

    [SerializeField] private Button createButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private GameObject joinInput;

    private async void Start(){
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

}
