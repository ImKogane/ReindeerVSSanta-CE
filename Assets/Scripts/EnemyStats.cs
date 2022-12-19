using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class EnemyStats : MonoBehaviour
{
    [Header("Enemy Stats")]

    public float PV = 100f;

    public float damage = 0f;

    [SerializeField]
    int pointsOnDeath;

    private WaveSpawner waveSpawner;

    //public PhotonView view;

    void Start()
    {
        waveSpawner = GameObject.FindWithTag("WaveManager").GetComponent<WaveSpawner>();
    }


    public void TakeDamage(float damage/*, PlayerStats playerDealDamage*/)
    {
        PV -= damage;

        if (PV <= 0)
        {
            //view = GetComponent<PhotonView>();
            //view.RPC("networkDestroy", RpcTarget.All);
            //playerDealDamage.AddPoints(pointsOnDeath);
            
        }
    }

    //[PunRPC]
    public void networkDestroy(){
        if (waveSpawner != null /*&& PhotonNetwork.IsMasterClient */== true){
            if (waveSpawner.EnemiesAlive != 0)
            {
                waveSpawner.EnemiesAlive -= 1;
            }
           /* PhotonNetwork.*/Destroy(gameObject);
        }
    }
}
