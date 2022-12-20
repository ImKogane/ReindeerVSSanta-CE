using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class PlayerNetwork : NetworkBehaviour
{

    [SerializeField] private Transform spawnObjectsPrefab;

    private Transform spawnedObjectTransform;

    private NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>(
        new MyCustomData{
            _x = 0,
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct MyCustomData : INetworkSerializable {
        public float _x;
        public FixedString128Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
            serializer.SerializeValue(ref _x);
            serializer.SerializeValue(ref message);
        }
    }

    public override void OnNetworkSpawn(){
        randomNumber.OnValueChanged += (MyCustomData previousValue, MyCustomData newValue) => {
            Debug.Log(OwnerClientId + "; randomNumber: " + newValue._x + " message: " + newValue.message);
        };
    }

    private void Update(){
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T)) {
            spawnedObjectTransform = Instantiate(spawnObjectsPrefab);
            spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);

            //TestClientRpc(new ClientRpcParams{ Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } }});
            // randomNumber.Value = new MyCustomData {
            //     _x = Random.Range(0, 100),
            //     message = new FixedString128Bytes("Hello World!"),
            // };
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            spawnedObjectTransform.GetComponent<NetworkObject>().Despawn(true);
            Destroy(spawnedObjectTransform.gameObject);
        }

        // Vector3 moveDir = new Vector3(0,0,0);

        // if (Input.GetKey(KeyCode.Z)) moveDir.z = +1f;
        // if (Input.GetKey(KeyCode.Q)) moveDir.x = -1f;
        // if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        // if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;

        // float moveSpeed = 3f;
        // transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    [ServerRpc]
    private void TestServerRpc(ServerRpcParams serverRpcParams) {
        Debug.Log("testServerRpc " + OwnerClientId + "; " +  serverRpcParams.Receive.SenderClientId);
    }

    [ClientRpc]
    private void TestClientRpc(ClientRpcParams clientRpcParams) {
        Debug.Log("testClientRpc ");
    }
}