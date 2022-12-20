using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using TMPro;


public class NetworkManagerUI : MonoBehaviour
{

    [SerializeField] private Button createButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private GameObject joinInput;

    private void Awake(){

        createButton.onClick.AddListener(() => {
            CreateRelay();
        });

        joinButton.onClick.AddListener(() => {
            JoinRelay(joinInput.GetComponent<TMP_InputField>().text);
        });

    }

    private async void Start(){
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private async void CreateRelay(){
        try {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            joinInput.GetComponent<TMP_InputField>().text = joinCode;
        } catch (RelayServiceException e){
            Debug.Log("Relay service error: " + e.Message);
        }
    }

    private async void JoinRelay(string joinCode){
        try {
            Debug.Log("JoinRelay: " + joinCode);
            RelayService.Instance.JoinAllocationAsync(joinCode);
        } catch (RelayServiceException e){
            Debug.Log("Relay service error: " + e.Message);
        }
    }

}
