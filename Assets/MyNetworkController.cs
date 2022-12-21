using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class MyNetworkController : NetworkBehaviour
{
    [SerializeField] InputActionReference _move;
    [SerializeField] CharacterController _controller;

    void Start()
    {

        if(IsOwner) return;
        _move.action.started += MovePlayer;
        _move.action.performed += MovePlayer;

    }


    void MovePlayer(InputAction.CallbackContext obj)
    {
        var tmp = obj.ReadValue<Vector2>();
        var dir = new Vector3(tmp.x, 0, tmp.y);
        MovePlayerServerRPC(dir);
    }

    [ServerRpc]
    void MovePlayerServerRPC(Vector3 dir)
    {
        _controller.Move(dir * Time.deltaTime* 10f);
    }


}
